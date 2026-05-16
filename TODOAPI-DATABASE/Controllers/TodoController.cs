using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TODOAPI_DATABASE.DTOs;
using TODOAPI_DATABASE.Responses;
using TODOAPI_DATABASE.Services;

namespace TODOAPI_DATABASE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
            private readonly ITodoService _service;

            public TodoController(ITodoService service)
            {
                _service = service;
            }
            // GET /api/todos
            [HttpGet]
            public IActionResult GetAll()
            {
                var todos = _service.GetAll();
                return Ok(todos);

            }
            // GET /api/todos/1
            [HttpGet("{id}")]
            public IActionResult GetById(int id)
            {
                var todo = _service.GetById(id);
                if (todo == null)
                    return NotFound(BuildErrorResponse(400, $"Todo with id {id} not found"));
                return Ok(todo);
            }
            // POST /api/todos
            [HttpPost]
            public IActionResult CreateTodo([FromBody] CreateTodoDTO dto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(BuildErrorResponse(
                        400, "Validation failed", GetValidationErrors()
                    ));
                var createTodo = _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = createTodo.Id }, createTodo);
            }

            // PUT /api/todos/1
            [HttpPut("{id}")]
            public IActionResult UpdateToDo(int id, [FromBody] UpdateTodoDTO dto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(BuildErrorResponse(
                        400, "Validation failed", GetValidationErrors()
                    ));

                var updateTodo = _service.Update(id, dto);
                if (updateTodo == null)
                    return NotFound(BuildErrorResponse(
                         404, $"Todo with id {id} not found"
                     ));
                return Ok(updateTodo);

            }
            // DELETE /api/todos/1
            [HttpDelete("{id}")]
            public IActionResult DeleteById(int id)
            {
                var success = _service.Delete(id);
                if (!success)
                    return NotFound(BuildErrorResponse(
                        404, $"Todo with id {id} not found"
                    ));
                return NoContent();
            }

            private ErrorResponse BuildErrorResponse(int statusCode, string message, Dictionary<string, string[]>? errors = null)
            {
                return new ErrorResponse
                {
                    StatusCode = statusCode,
                    Message = message,
                    Errors = errors,
                    Timestamp = DateTime.Now
                };
            }

            private Dictionary<string, string[]> GetValidationErrors()
            {
                return ModelState
                    .Where(e => e.Value != null && e.Value.Errors.Count > 0)
                    .ToDictionary(
                        e => e.Key,
                        e => e.Value!.Errors
                                 .Select(x => x.ErrorMessage)
                                 .ToArray()
                    );
            }

        

    }
}
