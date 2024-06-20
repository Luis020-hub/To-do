<template>
    <main class="row d-flex justify-content-center mt-2">
        <div class="col-12 col-md-10">
            <div id="content">
                <TodoItem v-for="todo in todos" :key="todo.id" :todo="todo" @openTodoDetails="openTodoDetails"
                    @editTodo="editTodo" @deleteTodo="deleteTodo" @toggleComplete="toggleComplete" />
            </div>
        </div>
    </main>
</template>

<script>
import TodoItem from './TodoItem.vue';

export default {
    props: ['todos'],
    components: {
        TodoItem
    },
    methods: {
        openTodoDetails(todo) {
            this.$emit('openTodoDetails', todo);
        },
        editTodo(todo) {
            this.$emit('editTodo', todo);
        },
        deleteTodo(todo) {
            this.$emit('deleteTodo', todo);
        },
        toggleComplete(todo) {
            this.$emit('toggleComplete', todo);
        }
    }
};
</script>

<style scoped>
.content-item {
    border-bottom: 1px solid rgba(108, 99, 255, 0.5);
    margin-right: 0;
}

.content-item:hover {
    background-color: #666;
}
</style>