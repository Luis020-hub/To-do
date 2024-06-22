import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5151/api',
});

export const getTodos = () => api.get('/todo');
export const getTodo = (id) => api.get(`/todo/${id}`);
export const createTodo = (todo) => api.post('/todo', todo);
export const updateTodo = (id, todo) => api.put(`/todo/${id}`, todo);
export const deleteTodo = (id) => api.delete(`/todo/${id}`);
export const updateTodoCompletionStatus = (id, isCompleted) => api.patch(`/todo/${id}/complete`, { isCompleted });

export default api;