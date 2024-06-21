<template>
    <div class="row content-item">
        <div class="col-1 mt-auto mb-auto">
            <input class="todo-check" type="checkbox" :checked="todo.isCompleted" @change="toggleComplete" />
        </div>
        <div class="col-5 col-sm-8 col-md-8 details mt-2" @click="showDetails" :class="{ completed: todo.isCompleted }">
            <span><b>{{ todo.title }}</b></span>
            <div class="mb-1">
                <small>{{ todo.time }}</small> <br>
                <small>{{ todo.date }}</small>
            </div>
        </div>
        <div class="col-5 col-sm-3 col-md-3 text-right mt-auto mb-auto">
            <i class="fa-solid fa-pencil mr-3" @click.stop="editTodo"></i>
            <i class="fa-solid fa-trash" @click.stop="deleteTodo"></i>
        </div>
    </div>
</template>

<script>
export default {
    props: ['todo'],
    methods: {
        showDetails() {
            this.$emit('openTodoDetails', this.todo);
        },
        toggleComplete() {
            this.$emit('toggleComplete', this.todo);
        },
        editTodo() {
            this.$emit('editTodo', this.todo);
        },
        deleteTodo() {
            this.$emit('deleteTodo', this.todo);
        }
    }
};
</script>

<style scoped>
.todo-check {
    cursor: pointer;
    transform: scale(1.5);
}

.fa-solid.fa-pencil,
.fa-solid.fa-trash {
    font-size: 1.4rem;
    color: #bbb;
    cursor: pointer;
}

.fa-solid.fa-pencil:hover {
    color: orange;
}

.fa-solid.fa-trash:hover {
    color: red;
}

.details {
    cursor: pointer;
}

.completed span,
.completed small {
    text-decoration: line-through;
    color: #888;
}
</style>