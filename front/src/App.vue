<template>
  <div class="d-flex justify-content-center">
    <div class="layout col-10">
      <Header @openAddTodoModal="openAddTodoModal" />
      <MainContent :todos="todos" @openTodoDetails="openTodoDetails" />
      <AppFooterLeft />
      <FooterContent @openAddTodoModal="openAddTodoModal" />
    </div>
  </div>
</template>

<script>
import Header from './components/Header.vue';
import MainContent from './components/MainContent.vue';
import AppFooterLeft from './components/AppFooterLeft.vue';
import FooterContent from './components/FooterContent.vue';
import Swal from 'sweetalert2';

export default {
  components: {
    Header,
    MainContent,
    AppFooterLeft,
    FooterContent
  },
  data() {
    return {
      todos: [],
      newTodo: {
        title: '',
        description: '',
        time: '',
        date: ''
      }
    };
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
    }
  }
};
</script>

<style>
@import '@/assets/style.css';
</style>
