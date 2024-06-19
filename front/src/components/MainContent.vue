<!-- components/MainContent.vue -->
<template>
    <main class="row d-flex justify-content-center mt-2">
        <div class="col-12 col-md-10">
            <div id="content">
                <TodoItem v-for="(todo, index) in todos" :key="index" :todo="todo" @click="openTodoDetails(todo)" />
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
        }
    }
};
</script>