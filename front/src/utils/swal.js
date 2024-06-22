import Swal from 'sweetalert2';

export function openAddTodoModal(callback) {
    Swal.fire({
        title: 'Add New Todo',
        html: `
            <div>
                <input id="swal-input1" class="swal2-input" placeholder="Title" required>
            </div>
            <div>
                <textarea id="swal-input2" class="swal2-textarea" placeholder="Description" required></textarea>
            </div>
            <div>
                <input id="swal-input3" class="swal2-input" type="time" placeholder="Time" required>
                <input id="swal-input4" class="swal2-input" type="date" placeholder="Date" required>
            </div>
        `,
        showCancelButton: true,
        focusConfirm: false,
        preConfirm: () => {
            const title = document.getElementById('swal-input1').value;
            const description = document.getElementById('swal-input2').value;
            const time = document.getElementById('swal-input3').value;
            const date = document.getElementById('swal-input4').value;

            if (!title || !description || !time || !date) {
                Swal.showValidationMessage('All fields are required');
                return false;
            }
            return {
                title: title,
                description: description,
                time: time,
                date: date
            };
        }
    }).then((result) => {
        if (result.isConfirmed) {
            callback(result.value);
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

export function openTodoDetails(todo) {
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

export function openEditTodoModal(todo, callback) {
    Swal.fire({
        title: 'Edit Todo',
        html: `
            <div>
                <input id="swal-input1" class="swal2-input" value="${todo.title}" required>
            </div>
            <div>
                <textarea id="swal-input2" class="swal2-textarea" required>${todo.description}</textarea>
            </div>
            <div>
                <input id="swal-input3" class="swal2-input" type="time" value="${todo.time}" required>
                <input id="swal-input4" class="swal2-input" type="date" value="${todo.date}" required>
            </div>
        `,
        showCancelButton: true,
        focusConfirm: false,
        preConfirm: () => {
            const title = document.getElementById('swal-input1').value;
            const description = document.getElementById('swal-input2').value;
            const time = document.getElementById('swal-input3').value;
            const date = document.getElementById('swal-input4').value;
            if (!title || !description || !time || !date) {
                Swal.showValidationMessage('All fields are required');
                return false;
            }
            return {
                title: title,
                description: description,
                time: time,
                date: date
            };
        }
    }).then((result) => {
        if (result.isConfirmed) {
            callback({ ...todo, ...result.value });
            Swal.fire({
                icon: 'success',
                title: 'Todo updated successfully!',
                showConfirmButton: false,
                timer: 1500
            });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire({
                icon: 'error',
                title: 'Todo not updated',
                showConfirmButton: false,
                timer: 1500
            });
        }
    });
}

export function confirmDeleteTodo(callback) {
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
            callback();
            Swal.fire(
                'Deleted!',
                'Your todo has been deleted.',
                'success'
            );
        }
    });
}