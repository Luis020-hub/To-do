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

        private bool ContainsHtmlTags(string input)
        {
            return HtmlRegex.IsMatch(input);
        }

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
            if (ContainsHtmlTags(todoItem.Title) || ContainsHtmlTags(todoItem.Description))
            {
                return BadRequest("Titles and descriptions cannot contain HTML tags.");
            }

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

            if (ContainsHtmlTags(todoItem.Title) || ContainsHtmlTags(todoItem.Description))
            {
                return BadRequest("Titles and descriptions cannot contain HTML tags.");
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

            string feedbackMessage = $"Todo Task with ID {existingTodoItem.Id} has been updated.";

            return Ok(new { message = feedbackMessage });
        }

        [HttpPatch("{id}/complete")]
        public IActionResult MarkAsComplete(int id, [FromBody] bool isCompleted)
        {
            var todoItem = todoItems.FirstOrDefault(t => t.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.IsCompleted = isCompleted;
            string feedbackMessage = $"Todo Task with ID {todoItem.Id} has been marked as {(isCompleted ? "completed" : "not completed")}.";

            return Ok(new { message = feedbackMessage });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(int id)
        {
            var todoItem = todoItems.FirstOrDefault(t => t.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            string deletedTitle = todoItem.Title;

            todoItems.Remove(todoItem);

            return Ok(new { message = $"Todo task '{deletedTitle}' with ID {id} has been deleted." });
        }
    }
}