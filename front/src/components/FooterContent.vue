<template>
    <div class="d-block d-md-none fixed-bottom left text-left p-3 d-flex justify-content-between">
        <i class="fa-solid" :class="{ 'fa-sun': theme === 'light', 'fa-moon': theme === 'dark' }"
            @click="toggleTheme"></i>
        <i class="fa-solid fa-plus" @click="openAddTodoModal"></i>
    </div>
</template>

<script>
export default {
    props: {
        theme: {
            type: String,
            required: true
        }
    },
    methods: {
        openAddTodoModal() {
            this.$emit('openAddTodoModal');
        },
        toggleTheme() {
            this.$emit('toggleTheme');
        }
    }
};
</script>

<style scoped>
.fixed-bottom.left i,
.fixed-bottom.right i {
    font-size: 2rem;
    color: var(--text-color);
    background-color: var(--primary-color);
    padding: 0.5rem;
    border-radius: 50%;
}
</style>