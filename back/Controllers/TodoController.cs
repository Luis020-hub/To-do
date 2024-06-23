using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using back.Models;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> todoItems = new List<TodoItem>();

        private static readonly Regex HtmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        private bool ContainsHtmlTags(string input) => HtmlRegex.IsMatch(input);

        private bool IsValidDate(string date, out DateTime parsedDate) =>
            DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);

        private bool IsValidTime(string time, out TimeSpan parsedTime) =>
            TimeSpan.TryParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture, TimeSpanStyles.None, out parsedTime);

        private bool IsValidTitleLength(string title) =>
            title.Length <= 40;

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodos() => Ok(todoItems);

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem(int id)
        {
            var todoItem = todoItems.FirstOrDefault(t => t.Id == id);
            if (todoItem == null) return NotFound();
            return Ok(todoItem);
        }

        [HttpPost]
        public ActionResult<TodoItem> PostTodoItem(TodoItem todoItem)
        {
            if (ContainsHtmlTags(todoItem.Title) || ContainsHtmlTags(todoItem.Description))
                return BadRequest("Titles and descriptions cannot contain HTML tags.");

            if (!IsValidDate(todoItem.Date, out DateTime parsedDate))
                return BadRequest("Invalid date format. Date should be in yyyy-MM-dd format.");

            if (!IsValidTime(todoItem.Time, out TimeSpan parsedTime))
                return BadRequest("Invalid time format. Time should be in hh:mm format.");

            if (!IsValidTitleLength(todoItem.Title))
                return BadRequest("Title must not exceed 40 characters.");

            todoItem.Date = parsedDate.ToString("yyyy-MM-dd");
            todoItem.Time = parsedTime.ToString(@"hh\:mm");
            todoItem.Id = todoItems.Count > 0 ? todoItems.Max(t => t.Id) + 1 : 1;
            todoItems.Add(todoItem);

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public IActionResult PutTodoItem(int id, [FromBody] TodoItem updatedTodoItem)
        {
            var existingTodoItem = todoItems.FirstOrDefault(t => t.Id == id);
            if (existingTodoItem == null) return NotFound();

            if (ContainsHtmlTags(updatedTodoItem.Title) || ContainsHtmlTags(updatedTodoItem.Description))
                return BadRequest("Titles and descriptions cannot contain HTML tags.");

            if (!IsValidDate(updatedTodoItem.Date, out DateTime parsedDate))
                return BadRequest("Invalid date format. Date should be in yyyy-MM-dd format.");

            if (!IsValidTime(updatedTodoItem.Time, out TimeSpan parsedTime))
                return BadRequest("Invalid time format. Time should be in hh:mm format.");

            if (!IsValidTitleLength(updatedTodoItem.Title))
                return BadRequest("Title must not exceed 40 characters.");

            existingTodoItem.Title = updatedTodoItem.Title;
            existingTodoItem.Description = updatedTodoItem.Description;
            existingTodoItem.IsCompleted = updatedTodoItem.IsCompleted;
            existingTodoItem.Date = parsedDate.ToString("yyyy-MM-dd");
            existingTodoItem.Time = parsedTime.ToString(@"hh\:mm");

            return Ok(new { message = $"Todo Task with ID {existingTodoItem.Id} has been updated." });
        }

        [HttpPatch("{id}/complete")]
        public IActionResult MarkAsComplete(int id, [FromBody] bool isCompleted)
        {
            var todoItem = todoItems.FirstOrDefault(t => t.Id == id);
            if (todoItem == null) return NotFound();

            todoItem.IsCompleted = isCompleted;

            return Ok(new { message = $"Todo Task with ID {todoItem.Id} has been marked as {(isCompleted ? "completed" : "not completed")}." });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(int id)
        {
            var todoItem = todoItems.FirstOrDefault(t => t.Id == id);
            if (todoItem == null) return NotFound();

            string deletedTitle = todoItem.Title;
            todoItems.Remove(todoItem);

            return Ok(new { message = $"Todo task '{deletedTitle}' with ID {id} has been deleted." });
        }
    }
}