namespace back.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool IsCompleted { get; set; }
        public required string Date { get; set; }
        public required string Time { get; set; }
    }
}
