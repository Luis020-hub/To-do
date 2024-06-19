<template>
    <header class="row" id="app-header">
        <div class="col-12 text-center mt-4" style="padding-right: 0px;">
            <h3>TODO LIST</h3>
            <div class="row input-group mt-4">
                <div class="input-group col-12" style="padding: 0;">
                    <input type="text" class="form-control" placeholder="Search...">
                    <div class="input-group-append">
                        <button type="button" class="btn add-todo btn-outline-secondary d-none d-md-block"
                            @click="openAddTodoModal">
                            <i class="fa-solid fa-plus"></i>
                        </button>
                        <button type="button" class="btn change-theme btn-outline-secondary d-none d-md-block">
                            <i class="fa-solid fa-sun"></i>
                        </button>
                        <button type="button"
                            class="btn filters btn-outline-secondary dropdown-toggle dropdown-toggle-split"
                            data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        </button>
                        <div class="dropdown-menu">
                            <a class="dropdown-item" href="#" @click="filter('Today')">Today</a>
                            <a class="dropdown-item" href="#" @click="filter('Next Days')">Next Days</a>
                            <a class="dropdown-item" href="#" @click="filter('Solved')">Solved</a>
                            <a class="dropdown-item" href="#" @click="filter('Unsolved')">Unsolved</a>
                            <a class="dropdown-item" href="#" @click="filter('Expired')">Expired</a>
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
    methods: {
        openAddTodoModal() {
            this.$emit('openAddTodoModal');
        },
        filter(criteria) {
            this.$emit('filterTodos', criteria);
        }
    }
};
</script>

<style scoped>
.add-todo,
.change-theme,
.filters {
    background-color: #6C63FF;
    color: #fff;
    border: 1px solid #fff;
}

.add-todo:hover {
    background-color: #008000;
}
</style>