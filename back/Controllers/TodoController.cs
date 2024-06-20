using back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> todoItems = new List<TodoItem>();

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodos()
        {
            return todoItems;
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem(int id)
        {
            var todoItem = todoItems.FirstOrDefault(t => t.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public ActionResult<TodoItem> PostTodoItem(TodoItem todoItem)
        {
            if (!DateTime.TryParseExact(todoItem.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return BadRequest("Invalid date format. Date should be in yyyy-MM-dd format.");
            }

            if (!TimeSpan.TryParseExact(todoItem.Time, "hh\\:mm", CultureInfo.InvariantCulture, TimeSpanStyles.None, out TimeSpan parsedTime))
            {
                return BadRequest("Invalid time format. Time should be in hh:mm format.");
            }

            todoItem.Date = parsedDate.ToString("yyyy-MM-dd");
            todoItem.Time = parsedTime.ToString(@"hh\:mm");
            todoItem.Id = todoItems.Count > 0 ? todoItems.Max(t => t.Id) + 1 : 1;
            todoItems.Add(todoItem);

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public IActionResult PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            var existingTodoItem = todoItems.FirstOrDefault(t => t.Id == id);
            if (existingTodoItem == null)
            {
                return NotFound();
            }

            if (!DateTime.TryParseExact(todoItem.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return BadRequest("Invalid date format. Date should be in yyyy-MM-dd format.");
            }

            if (!TimeSpan.TryParseExact(todoItem.Time, "hh\\:mm", CultureInfo.InvariantCulture, TimeSpanStyles.None, out TimeSpan parsedTime))
            {
                return BadRequest("Invalid time format. Time should be in hh:mm format.");
            }

            existingTodoItem.Title = todoItem.Title;
            existingTodoItem.Description = todoItem.Description;
            existingTodoItem.IsCompleted = todoItem.IsCompleted;
            existingTodoItem.Date = parsedDate.ToString("yyyy-MM-dd");
            existingTodoItem.Time = parsedTime.ToString(@"hh\:mm");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(int id)
        {
            var todoItem = todoItems.FirstOrDefault(t => t.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItems.Remove(todoItem);
            return NoContent();
        }
    }
}