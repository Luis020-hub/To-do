<template>
  <div class="d-flex justify-content-center">
    <div class="layout col-10">
      <Header @openAddTodoModal="openAddTodoModal" />
      <MainContent :todos="todos" @openTodoDetails="openTodoDetails" @editTodo="openEditTodoModal"
        @deleteTodo="confirmDeleteTodo" />
      <FooterContent @openAddTodoModal="openAddTodoModal" />
    </div>
  </div>
</template>

<script>
import Header from './components/Header.vue';
import MainContent from './components/MainContent.vue';
import FooterContent from './components/FooterContent.vue';
import Swal from 'sweetalert2';

export default {
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
        date: ''
      },
      editedTodo: null
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
      }
    }
  }
};
</script>

<style>
@import '@/assets/style.css';
</style>