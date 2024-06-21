<template>
  <div>
    <div class="d-flex justify-content-center">
      <div class="layout col-10">
        <Header @openAddTodoModal="openAddTodoModal" @toggleTheme="toggleTheme" :theme="theme" :todos="todos" @filterTodos="filterTodos" @updateSearchQuery="updateSearchQuery" />
        <MainContent :todos="filteredTodos" @openTodoDetails="openTodoDetails" @editTodo="openEditTodoModal" @deleteTodo="confirmDeleteTodo" @toggleComplete="toggleComplete" />
        <FooterContent @openAddTodoModal="openAddTodoModal" @toggleTheme="toggleTheme" :theme="theme" />
      </div>
    </div>
  </div>
</template>

<script>
import { defineComponent } from 'vue';
import Header from './components/Header.vue';
import MainContent from './components/MainContent.vue';
import FooterContent from './components/FooterContent.vue';
import { openAddTodoModal, openEditTodoModal, openTodoDetails, confirmDeleteTodo } from './utils/swal';
import { filterToday, filterNextDays, filterCompleted, filterUnsolved } from './utils/filterTodos';
import { getTodos, createTodo, updateTodo, deleteTodo } from './utils/api';

export default defineComponent({
  components: {
    Header,
    MainContent,
    FooterContent
  },
  data() {
    return {
      todos: [],
      currentFilter: 'All',
      searchQuery: '',
      theme: 'light'
    };
  },
  computed: {
    filteredTodos() {
      let filtered = this.todos;

      if (this.searchQuery) {
        filtered = filtered.filter(todo =>
          todo.title.toLowerCase().includes(this.searchQuery.toLowerCase())
        );
      }

      switch (this.currentFilter) {
        case 'Today':
          filtered = filterToday(filtered);
          break;
        case 'Next Days':
          filtered = filterNextDays(filtered);
          break;
        case 'Solved':
          filtered = filterCompleted(filtered);
          break;
        case 'Unsolved':
          filtered = filterUnsolved(filtered);
          break;
        case 'All':
        default:
          break;
      }

      return filtered;
    }
  },
  created() {
    this.fetchTodos();
  },
  methods: {
    async fetchTodos() {
      try {
        const response = await getTodos();
        this.todos = response.data;
      } catch (error) {
        console.error("Erro ao buscar todos:", error);
      }
    },
    openAddTodoModal() {
      openAddTodoModal(async newTodo => {
        try {
          const response = await createTodo(newTodo);
          this.todos.push(response.data);
        } catch (error) {
          console.error("Erro ao adicionar todo:", error);
        }
      });
    },
    openTodoDetails(todo) {
      openTodoDetails(todo);
    },
    openEditTodoModal(todo) {
      openEditTodoModal(todo, async updatedTodo => {
        try {
          await updateTodo(updatedTodo.id, updatedTodo);
          const index = this.todos.findIndex(t => t.id === updatedTodo.id);
          if (index !== -1) {
            this.todos.splice(index, 1, updatedTodo);
          }
        } catch (error) {
          console.error("Erro ao editar todo:", error);
        }
      });
    },
    confirmDeleteTodo(todo) {
      confirmDeleteTodo(async () => {
        try {
          await deleteTodo(todo.id);
          this.todos = this.todos.filter(t => t.id !== todo.id);
        } catch (error) {
          console.error("Erro ao deletar todo:", error);
        }
      });
    },
    async toggleComplete(todo) {
      const updatedTodo = { ...todo, isCompleted: !todo.isCompleted };
      try {
        await updateTodo(updatedTodo.id, updatedTodo);
        const index = this.todos.findIndex(t => t.id === updatedTodo.id);
        if (index !== -1) {
          this.todos.splice(index, 1, updatedTodo);
        }
      } catch (error) {
        console.error("Erro ao atualizar status de completado:", error);
      }
    },
    filterTodos(criteria) {
      this.currentFilter = criteria;
    },
    updateSearchQuery(query) {
      this.searchQuery = query;
    },
    toggleTheme() {
      this.theme = this.theme === 'light' ? 'dark' : 'light';
    }
  }
});
</script>

<style>
:root {
  --primary-color: #6C63FF;
  --text-color: #000;
  --background-color: #fff;
}

.dark {
  --primary-color: #6C63FF;
  --text-color: #eee;
  --background-color: #222;
}

.light {
  --primary-color: #6C63FF;
  --text-color: #000;
  --background-color: #fff;
}

body {
  background-color: var(--background-color);
  color: var(--text-color);
}
</style>