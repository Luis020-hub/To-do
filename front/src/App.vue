<template>
  <div>
    <div class="d-flex justify-content-center">
      <div class="layout col-10">
        <Header @openAddTodoModal="openAddTodoModal" @toggleTheme="toggleTheme" :theme="theme"
          @filterTodos="filterTodos" @updateSearchQuery="updateSearchQuery" />
        <MainContent :todos="filteredTodos" @openTodoDetails="openTodoDetails" @editTodo="openEditTodoModal"
          @deleteTodo="confirmDeleteTodo" @toggleComplete="toggleComplete" />
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

export default defineComponent({
  components: {
    Header,
    MainContent,
    FooterContent
  },
  data() {
    return {
      todos: [],
      newTodo: {
        title: '',
        description: '',
        time: '',
        date: '',
        completed: false
      },
      editedTodo: null,
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
        case 'Today': {
          const yesterday = new Date();
          yesterday.setDate(yesterday.getDate() - 1);
          yesterday.setHours(0, 0, 0, 0);

          filtered = filtered.filter(todo => {
            const todoDate = new Date(todo.date);
            return (
              todoDate.getFullYear() === yesterday.getFullYear() &&
              todoDate.getMonth() === yesterday.getMonth() &&
              todoDate.getDate() === yesterday.getDate()
            );
          });
          break;
        }
        case 'Next Days': {
          const today = new Date();
          today.setHours(0, 0, 0, 0);

          filtered = filtered.filter(todo => {
            const todoDate = new Date(todo.date);
            return todoDate >= today;
          });
          break;
        }
        case 'Solved':
          filtered = filtered.filter(todo => todo.completed);
          break;
        case 'Unsolved':
          filtered = filtered.filter(todo => !todo.completed);
          break;
        case 'All':
        default:
          break;
      }

      return filtered;
    }
  },
  watch: {
    theme(newTheme) {
      document.body.className = newTheme;
    }
  },
  methods: {
    openAddTodoModal() {
      openAddTodoModal(newTodo => {
        this.todos.push(newTodo);
        this.clearNewTodo();
      });
    },
    clearNewTodo() {
      this.newTodo = {
        title: '',
        description: '',
        time: '',
        date: '',
        completed: false
      };
    },
    openTodoDetails(todo) {
      openTodoDetails(todo);
    },
    openEditTodoModal(todo) {
      openEditTodoModal(todo, updatedTodo => {
        const index = this.todos.findIndex(t => t === todo);
        if (index !== -1) {
          this.todos.splice(index, 1, updatedTodo);
        }
      });
    },
    confirmDeleteTodo(todo) {
      confirmDeleteTodo(todo, () => {
        const index = this.todos.findIndex(t => t === todo);
        if (index !== -1) {
          this.todos.splice(index, 1);
        }
      });
    },
    filterTodos(filter) {
      this.currentFilter = filter;
    },
    updateSearchQuery(query) {
      this.searchQuery = query;
    },
    toggleComplete(todo) {
      const index = this.todos.findIndex(t => t === todo);
      if (index !== -1) {
        this.todos[index].completed = !this.todos[index].completed;
      }
    },
    toggleTheme() {
      this.theme = this.theme === 'light' ? 'dark' : 'light';
    }
  },
  mounted() {
    document.body.className = this.theme;
  }
});
</script>

<style>
:root {
  --primary-color: #6C63FF;
  --text-color: #fff;
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