<template>
    <div>
        <div class="row content-item" v-for="(todo, index) in todos" :key="index" @click="openTodoDetails(todo)">
            <div class="col-1 mt-auto mb-auto">
                <input class="todo-check" type="checkbox">
            </div>
            <div class="col-5 col-sm-8 col-md-8 mt-2">
                <span><b>{{ todo.title }}</b></span>
                <div class="mb-1">
                    <small>{{ todo.time }}</small> <br>
                    <small>{{ todo.date }}</small>
                </div>
            </div>
            <div class="col-5 col-sm-3 col-md-3 text-right mt-auto mb-auto">
                <i class="fa-solid fa-pencil mr-3"></i>
                <i class="fa-solid fa-trash"></i>
            </div>
        </div>
    </div>
</template>

<script>
import Swal from 'sweetalert2';

export default {
    props: ['todos'],
    methods: {
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