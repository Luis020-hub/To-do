<template>
    <header class="row" id="app-header">
        <div class="col-12 text-center mt-4" style="padding-right: 0px;">
            <h3>TODO LIST</h3>
            <div class="row input-group mt-4">
                <div class="input-group col-12" style="padding: 0;">
                    <input type="text" class="form-control" placeholder="Search..." v-model="searchQuery" @input="updateSearchQuery">
                    <div class="input-group-append">
                        <button type="button" class="btn add-todo btn-outline-secondary d-none d-md-block" @click="openAddTodoModal">
                            <i class="fa-solid fa-plus"></i>
                        </button>
                        <button type="button" class="btn change-theme btn-outline-secondary d-none d-md-block" @click="toggleTheme">
                            <i class="fa-solid" :class="{ 'fa-sun': theme === 'light', 'fa-moon': theme === 'dark' }"></i>
                        </button>
                        <button type="button"
                            class="btn filters btn-outline-secondary dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        </button>
                        <div class="dropdown-menu">
                            <a class="dropdown-item" href="#" @click="filter('All')" :class="{ active: currentFilter === 'All' }">All</a>
                            <a class="dropdown-item" href="#" @click="filter('Today')" :class="{ active: currentFilter === 'Today' }">Today</a>
                            <a class="dropdown-item" href="#" @click="filter('Next Days')" :class="{ active: currentFilter === 'Next Days' }">Next Days</a>
                            <a class="dropdown-item" href="#" @click="filter('Solved')" :class="{ active: currentFilter === 'Solved' }">Solved</a>
                            <a class="dropdown-item" href="#" @click="filter('Unsolved')" :class="{ active: currentFilter === 'Unsolved' }">Unsolved</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </header>
</template>

<script>
export default {
    name: 'AppHeader',
    props: {
        theme: {
            type: String,
            required: true
        }
    },
    data() {
        return {
            searchQuery: '',
            currentFilter: 'All'
        };
    },
    methods: {
        openAddTodoModal() {
            this.$emit('openAddTodoModal');
        },
        filter(criteria) {
            this.currentFilter = criteria;
            this.$emit('filterTodos', criteria);
        },
        toggleTheme() {
            this.$emit('toggleTheme');
        },
        updateSearchQuery() {
            this.$emit('updateSearchQuery', this.searchQuery);
        }
    }
};
</script>

<style scoped>
.add-todo,
.change-theme,
.filters {
    background-color: var(--primary-color);
    color: var(--text-color);
    border: 1px solid var(--text-color);
}

.add-todo:hover {
    background-color: #008000;
}
</style>
