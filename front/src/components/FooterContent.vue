<template>
    <div>
        <footer class="d-block d-md-none fixed-bottom left text-left p-3">
            <i class="fa-solid" :class="{ 'fa-sun': theme === 'light', 'fa-moon': theme === 'dark' }"></i>
        </footer>
        <footer class="d-block d-md-none fixed-bottom right text-right p-3" @click="openAddTodoModal">
            <i class="fa-solid fa-plus"></i>
        </footer>
    </div>
</template>

<script>
export default {
    props: {
        theme: {
            type: String,
            required: true
        },
        toggleTheme: {
            type: Function,
            required: true
        }
    },
    computed: {
        themeIcon() {
            return this.theme === 'light' ? 'fa-solid fa-moon' : 'fa-solid fa-sun';
        }
    },
    methods: {
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
    background-color: var(--primary-color);
    padding: 0.5rem;
    border-radius: 50%;
}
</style>