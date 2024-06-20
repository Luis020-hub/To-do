import axios from 'axios';

const api = axios.create({
    baseURL: process.env.VUE_APP_API_BASE_URL || 'http://localhost:8081/api',
});

export const getTodos = () => api.get('/todo');
export const getTodo = (id) => api.get(`/todo/${id}`);
export const createTodo = (todo) => api.post('/todo', todo);
export const updateTodo = (id, todo) => api.put(`/todo/${id}`, todo);
export const deleteTodo = (id) => api.delete(`/todo/${id}`);

export default api;