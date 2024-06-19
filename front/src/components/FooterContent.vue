<template>
    <div>
        <footer class="d-block d-md-none fixed-bottom left text-left p-3">
            <i class="fa-solid fa-moon" @click="toggleTheme"></i>
        </footer>
        <footer class="d-block d-md-none fixed-bottom right text-right p-3" @click="openAddTodoModal">
            <i class="fa-solid fa-plus"></i>
        </footer>
    </div>
</template>

<script>
export default {
    methods: {
        toggleTheme() {
            console.log('Toggle theme');
        },
        openAddTodoModal(event) {
            if (event.target.classList.contains('fa-plus')) {
                this.$emit('openAddTodoModal');
            }
        }
    }
};
</script>

<style scoped>
.fixed-bottom.left i,
.fixed-bottom.right i {
    font-size: 2rem;
    color: #fff;
    background-color: #6C63FF;
    padding: 0.5rem;
    border-radius: 50%;
}
</style>