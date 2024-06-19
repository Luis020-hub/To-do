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
                            <a class="dropdown-item" href="#">Today</a>
                            <a class="dropdown-item" href="#">Next Days</a>
                            <a class="dropdown-item" href="#">Solved</a>
                            <a class="dropdown-item" href="#">Unsolved</a>
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
        }
    }
};
</script>