export function filterToday(todos) {
    const yesterday = new Date();
    yesterday.setDate(yesterday.getDate() - 1);
    yesterday.setHours(0, 0, 0, 0);

    return todos.filter(todo => {
        const todoDate = new Date(todo.date);
        return (
            todoDate.getFullYear() === yesterday.getFullYear() &&
            todoDate.getMonth() === yesterday.getMonth() &&
            todoDate.getDate() === yesterday.getDate()
        );
    });
}

export function filterNextDays(todos) {
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    return todos.filter(todo => {
        const todoDate = new Date(todo.date);
        return todoDate >= today;
    });
}

export function filterCompleted(todos) {
    return todos.filter(todo => todo.isCompleted);
}

export function filterUnsolved(todos) {
    return todos.filter(todo => !todo.isCompleted);
}
