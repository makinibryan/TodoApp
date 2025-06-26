using Microsoft.AspNetCore.Mvc;
using TodoApp.Contracts;
using TodoApp.Interfaces;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoServices _todoServices;

        public TodoController(ITodoServices todoServices)
        {
            _todoServices = todoServices;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTodoAsyc( CreateTodoRequest request)
        {
            if(!ModelState.IsValid) 
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    await _todoServices.CreateTodoAsync(request);
                    return Ok(new {message = "Blog post successful created"});
                }
                catch (Exception ex)
                {

                    return StatusCode(500, new {message = "An error occurred while creating the Todo Item", error = ex.Message});
                }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var todo = await _todoServices.GetAllAsync();
                if(todo == null || !todo.Any())
                {
                    return Ok(new { message = "No Todo Items for found" });
                }
                return Ok(new {message ="Successfully retrieved todos", data = todo});
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "An error occurred hile retrieving all Tood it posts", error = ex.Message });
            }
        }
        [HttpGet("{id:guid}")]

        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var todo = await _todoServices.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"No Todo item with Id: {id} found" });
                }
                return Ok( new {message = $"Succefully retrieved Todo item with Id: {id}", data =todo});
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = $"An error occurred while retrieving the Todo item with Id {id}, error = ex.Message ", error = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var todo = await _todoServices.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"Todo Item  with id {id} not found" });
                }
                await _todoServices.UpdateTodoAsync(id, request);

                return Ok(new {message = $"Todo Item  with id {id} not found" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = $"An error occurred while updating blog post with id {id}", error = ex.Message });
            }
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTodoAsyc(Guid id)
        {
            try
            {
                await _todoServices.DeleteTodoAsync(id);
                return Ok(new {message = $"Todo  with id {{id}} successfully deleted\"" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new {message = $"An error occurred while deleting Todo Item  with id {id}", error = ex.Message });
            }
        }
    }
}
