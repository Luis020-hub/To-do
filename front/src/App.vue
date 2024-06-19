<template>
  <div>
    <div class="d-flex justify-content-center">
      <div class="layout col-10">
        <Header @openAddTodoModal="openAddTodoModal" @filterTodos="filterTodos" @toggleTheme="toggleTheme"
          :theme="theme" />
        <MainContent :todos="filteredTodos" @openTodoDetails="openTodoDetails" @editTodo="openEditTodoModal"
          @deleteTodo="confirmDeleteTodo" />
        <FooterContent @openAddTodoModal="openAddTodoModal" @toggleTheme="toggleTheme" :theme="theme" />
      </div>
    </div>
  </div>
</template>

<script>
import Header from './components/Header.vue';
import MainContent from './components/MainContent.vue';
import FooterContent from './components/FooterContent.vue';
import Swal from 'sweetalert2';
import moment from 'moment';

export default {
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
      theme: 'light' // Estado do tema
    };
  },
  watch: {
    theme(newTheme) {
      document.body.className = newTheme;
    }
  },
  methods: {
    openAddTodoModal() {
      Swal.fire({
        title: 'Add New Todo',
        html: `
          <div>
            <input id="swal-input1" class="swal2-input" placeholder="Title">
          </div>
          <div>
            <textarea id="swal-input2" class="swal2-textarea" placeholder="Description"></textarea>
          </div>
          <div>
            <input id="swal-input3" class="swal2-input" type="time" placeholder="Time">
            <input id="swal-input4" class="swal2-input" type="date" placeholder="Date">
          </div>
        `,
        showCancelButton: true,
        focusConfirm: false,
        preConfirm: () => {
          const popup = Swal.getPopup();
          this.newTodo = {
            title: popup.querySelector('#swal-input1').value,
            description: popup.querySelector('#swal-input2').value,
            time: popup.querySelector('#swal-input3').value,
            date: popup.querySelector('#swal-input4').value
          };
        }
      }).then((result) => {
        if (result.isConfirmed) {
          this.todos.push({ ...this.newTodo });
          this.clearNewTodo();
          this.filterTodos(this.currentFilter);
          Swal.fire({
            icon: 'success',
            title: 'Todo added successfully!',
            showConfirmButton: false,
            timer: 1500
          });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
          Swal.fire({
            icon: 'error',
            title: 'Todo not added',
            showConfirmButton: false,
            timer: 1500
          });
        }
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
      Swal.fire({
        title: todo.title,
        html: `
          <div>
            <p><strong>Description:</strong> ${todo.description}</p>
            <p><strong>Time:</strong> ${todo.time}</p>
            <p><strong>Date:</strong> ${todo.date}</p>
          </div>
        `
      });
    },
    openEditTodoModal(todo) {
      this.editedTodo = { ...todo };
      Swal.fire({
        title: 'Edit Todo',
        html: `
          <div>
            <input id="swal-input1" class="swal2-input" value="${todo.title}">
          </div>
          <div>
            <textarea id="swal-input2" class="swal2-textarea">${todo.description}</textarea>
          </div>
          <div>
            <input id="swal-input3" class="swal2-input" type="time" value="${todo.time}">
            <input id="swal-input4" class="swal2-input" type="date" value="${todo.date}">
          </div>
        `,
        showCancelButton: true,
        focusConfirm: false,
        preConfirm: () => {
          const popup = Swal.getPopup();
          this.editedTodo.title = popup.querySelector('#swal-input1').value;
          this.editedTodo.description = popup.querySelector('#swal-input2').value;
          this.editedTodo.time = popup.querySelector('#swal-input3').value;
          this.editedTodo.date = popup.querySelector('#swal-input4').value;
        }
      }).then((result) => {
        if (result.isConfirmed) {
          const index = this.todos.findIndex(t => t === todo);
          if (index !== -1) {
            this.todos.splice(index, 1, this.editedTodo);
            this.filterTodos(this.currentFilter);
            Swal.fire({
              icon: 'success',
              title: 'Todo updated successfully!',
              showConfirmButton: false,
              timer: 1500
            });
          }
        } else if (result.dismiss === Swal.DismissReason.cancel) {
          Swal.fire({
            icon: 'error',
            title: 'Todo not updated',
            showConfirmButton: false,
            timer: 1500
          });
        }
      });
    },
    confirmDeleteTodo(todo) {
      Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
      }).then((result) => {
        if (result.isConfirmed) {
          this.deleteTodo(todo);
          Swal.fire(
            'Deleted!',
            'Your todo has been deleted.',
            'success'
          );
        }
      });
    },
    deleteTodo(todo) {
      const index = this.todos.findIndex(t => t === todo);
      if (index !== -1) {
        this.todos.splice(index, 1);
        this.filterTodos(this.currentFilter);
      }
    },
    filterTodos(filter) {
      this.currentFilter = filter;
      const today = moment().format('YYYY-MM-DD');
      const nextDays = moment().add(7, 'days').format('YYYY-MM-DD');

      switch (filter) {
        case 'Today':
          this.filteredTodos = this.todos.filter(todo => todo.date === today && !todo.completed);
          break;
        case 'Next Days':
          this.filteredTodos = this.todos.filter(todo => moment(todo.date).isBetween(today, nextDays) && !todo.completed);
          break;
        case 'Solved':
          this.filteredTodos = this.todos.filter(todo => todo.solved && !todo.completed);
          break;
        case 'Unsolved':
          this.filteredTodos = this.todos.filter(todo => !todo.completed);
          break;
        case 'Expired':
          this.filteredTodos = this.todos.filter(todo => moment(todo.date).isBefore(today) && !todo.completed);
          break;
        default:
          this.filteredTodos = this.todos;
      }
    },
    toggleTheme() {
      this.theme = this.theme === 'light' ? 'dark' : 'light';
    }
  },
  mounted() {
    document.body.className = this.theme;
    this.filterTodos(this.currentFilter);
  }
};
</script>

<style>
:root {
  --primary-color: #6C63FF;
  --text-color: #fff;
  --background-color: #fff;
}

.dark {
  --primary-color: #333;
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