<template>
    <main class="row d-flex justify-content-center mt-2">
        <div class="col-12 col-md-10">
            <div id="content">
                <TodoItem v-for="(todo, index) in todos" :key="index" :todo="todo" @openTodoDetails="openTodoDetails"
                    @editTodo="editTodo" @deleteTodo="deleteTodo" />
            </div>
        </div>
    </main>
</template>

<script>
import TodoItem from './TodoItem.vue';

export default {
    components: {
        TodoItem
    },
    props: {
        todos: {
            type: Array,
            required: true
        }
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