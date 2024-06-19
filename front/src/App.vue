<template>
  <div>
    <div class="d-flex justify-content-center">
      <div class="layout col-10">
        <Header @openAddTodoModal="openAddTodoModal" @toggleTheme="toggleTheme" :theme="theme" />
        <MainContent :todos="filteredTodos" @openTodoDetails="openTodoDetails" @editTodo="openEditTodoModal"
          @deleteTodo="confirmDeleteTodo" />
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
      filteredTodos: [],
      newTodo: {
        title: '',
        description: '',
        time: '',
        date: ''
      },
      editedTodo: null,
      currentFilter: 'All',
      theme: 'light'
    };
  },
  watch: {
    theme(newTheme) {
      document.body.className = newTheme;
    }
  },
  methods: {
    openAddTodoModal() {
      openAddTodoModal((newTodo) => {
        this.todos.push(newTodo);
        this.clearNewTodo();
        this.filterTodos(this.currentFilter);
      });
    },
    clearNewTodo() {
      this.newTodo = {
        title: '',
        description: '',
        time: '',
        date: ''
      };
    },
    openTodoDetails(todo) {
      openTodoDetails(todo);
    },
    openEditTodoModal(todo) {
      openEditTodoModal(todo, (updatedTodo) => {
        const index = this.todos.findIndex(t => t === todo);
        if (index !== -1) {
          this.todos.splice(index, 1, updatedTodo);
          this.filterTodos(this.currentFilter);
        }
      });
    },
    confirmDeleteTodo(todo) {
      confirmDeleteTodo(todo, () => {
        const index = this.todos.findIndex(t => t === todo);
        if (index !== -1) {
          this.todos.splice(index, 1);
          this.filterTodos(this.currentFilter);
        }
      });
    },
    filterTodos(filter) {
      this.currentFilter = filter;
      if (filter === 'All') {
        this.filteredTodos = [...this.todos];
      } else if (filter === 'Completed') {
        this.filteredTodos = this.todos.filter(todo => todo.completed);
      } else if (filter === 'Pending') {
        this.filteredTodos = this.todos.filter(todo => !todo.completed);
      }
    },
    toggleTheme() {
      this.theme = this.theme === 'light' ? 'dark' : 'light';
    }
  },
  mounted() {
    document.body.className = this.theme;
    this.todos = [];
    this.filterTodos(this.currentFilter);
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