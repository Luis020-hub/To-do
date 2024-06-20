namespace back.Models
{
    public class TodoContext
    {
        public List<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}