using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services;

namespace Todo.API.Controllers
{
    public record TodoRequest(string Title, bool IsCompleted);

    [Route("api/v1/todos")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _todoService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var todo = await _todoService.GetById(id);
            return todo == null ? NotFound() : Ok(todo);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] TodoRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                return BadRequest("Title is required.");

            var todo = new global::Todo.Domain.Todo
            {
                Title = request.Title.Trim(),
                IsCompleted = request.IsCompleted
            };
            await _todoService.AddTodo(todo);
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TodoRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                return BadRequest("Title is required.");

            var todo = await _todoService.GetById(id);
            if (todo == null) return NotFound();

            todo.Title = request.Title.Trim();
            todo.IsCompleted = request.IsCompleted;
            await _todoService.UpdateTodo(todo);
            return Ok(todo);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _todoService.GetById(id);
            if (todo == null) return NotFound();

            await _todoService.DeleteTodo(id);
            return NoContent();
        }
    }
}