import Swal from 'sweetalert2';

export function openAddTodoModal(callback) {
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
            return {
                title: popup.querySelector('#swal-input1').value,
                description: popup.querySelector('#swal-input2').value,
                time: popup.querySelector('#swal-input3').value,
                date: popup.querySelector('#swal-input4').value
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
            return {
                title: popup.querySelector('#swal-input1').value,
                description: popup.querySelector('#swal-input2').value,
                time: popup.querySelector('#swal-input3').value,
                date: popup.querySelector('#swal-input4').value
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

export function confirmDeleteTodo(todo, callback) {
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