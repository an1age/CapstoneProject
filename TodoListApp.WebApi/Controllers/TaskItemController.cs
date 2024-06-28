using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Models.Entities;
using TodoListApp.WebApi.Services;

namespace TodoListApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemDatabaseService _taskItemDatabaseService;

        public TaskItemController(ITaskItemDatabaseService taskItemDatabaseService)
        {
            this._taskItemDatabaseService = taskItemDatabaseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
        {
            var taskItems = await this._taskItemDatabaseService.GetTaskItemsAsync();
            return this.Ok(taskItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
        {
            var taskItem = await this._taskItemDatabaseService.GetTaskItemByIdAsync(id);

            if (taskItem == null)
            {
                return this.NotFound();
            }

            return this.Ok(taskItem);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItem taskItem)
        {
            var createdTaskItem = await this._taskItemDatabaseService.AddTaskItemAsync(taskItem);
            return this.CreatedAtAction(nameof(this.GetTaskItem), new { id = createdTaskItem.Id }, createdTaskItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskItem(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return this.BadRequest();
            }

            await this._taskItemDatabaseService.UpdateTaskItemAsync(taskItem);
            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var result = await this._taskItemDatabaseService.DeleteTaskItemAsync(id);

            if (!result)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }
    }
}
