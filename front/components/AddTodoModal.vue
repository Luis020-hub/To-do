<template>
    <button type="button" class="btn add-todo btn-outline-secondary d-none d-md-block" @click="openAddTodoModal">
        <i class="fa-solid fa-plus"></i>
    </button>
</template>

<script>
import Swal from 'sweetalert2';

export default {
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
                    const newTodo = {
                        title: popup.querySelector('#swal-input1').value,
                        description: popup.querySelector('#swal-input2').value,
                        time: popup.querySelector('#swal-input3').value,
                        date: popup.querySelector('#swal-input4').value
                    };
                    this.$emit('add-todo', newTodo);
                }
            }).then((result) => {
                if (result.isConfirmed) {
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
        }
    }
};
</script>