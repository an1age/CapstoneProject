using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Models.Entities;
using TodoListApp.WebApi.Services;

namespace TodoListApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListDatabaseService _todoListDatabaseService;

        public TodoListController(ITodoListDatabaseService todoListDatabaseService)
        {
            this._todoListDatabaseService = todoListDatabaseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoList>>> GetTodoLists()
        {
            var todoLists = await this._todoListDatabaseService.GetTodoListsAsync();
            return this.Ok(todoLists);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoList>> GetTodoList(int id)
        {
            var todoList = await this._todoListDatabaseService.GetTodoListByIdAsync(id);

            if (todoList == null)
            {
                return this.NotFound();
            }

            return this.Ok(todoList);
        }

        [HttpPost]
        public async Task<ActionResult<TodoList>> PostTodoList(TodoList todoList)
        {
            var createdTodoList = await this._todoListDatabaseService.AddTodoListAsync(todoList);
            return this.CreatedAtAction(nameof(this.GetTodoList), new { id = createdTodoList.Id }, createdTodoList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoList(int id, TodoList todoList)
        {
            if (id != todoList.Id)
            {
                return this.BadRequest();
            }

            await this._todoListDatabaseService.UpdateTodoListAsync(todoList);
            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            var result = await this._todoListDatabaseService.DeleteTodoListAsync(id);

            if (!result)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }
    }
}
