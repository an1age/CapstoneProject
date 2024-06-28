using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Models.Entities;
using TodoListApp.WebApp.Services;

namespace TodoListApp.WebApp.Controllers
{
    public class TaskItemController : Controller
    {
        private readonly ITaskItemWebApiService _taskItemService;

        public TaskItemController(ITaskItemWebApiService taskItemService)
        {
            this._taskItemService = taskItemService;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await this._taskItemService.GetTaskItemsAsync();
            return this.View(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleCompleted(int id, bool isCompleted)
        {
            var taskItem = await this._taskItemService.GetTaskItemByIdAsync(id);
            if (taskItem == null)
            {
                return this.NotFound();
            }

            taskItem.IsCompleted = isCompleted;
            await this._taskItemService.UpdateTaskItemAsync(taskItem);

            return this.Ok();
        }

        public IActionResult CreateTask(int todoListId)
        {
            var taskItem = new TaskItem { TodoListId = todoListId };
            return this.View(taskItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskItem taskItem)
        {
            if (this.ModelState.IsValid)
            {
                taskItem.IsCompleted = false;
                await this._taskItemService.CreateTaskItemAsync(taskItem);
                return this.RedirectToAction("Details", "TodoList", new { id = taskItem.TodoListId });
            }

            return this.View(taskItem);
        }

        public async Task<IActionResult> EditTask(int id)
        {
            var taskItem = await this._taskItemService.GetTaskItemByIdAsync(id);
            if (taskItem == null)
            {
                return this.NotFound();
            }

            return this.View(taskItem);
        }

        [HttpPost]
        public async Task<IActionResult> EditTask(TaskItem taskItem)
        {
            if (this.ModelState.IsValid)
            {
                var existingTask = await this._taskItemService.GetTaskItemByIdAsync(taskItem.Id);
                if (existingTask != null)
                {
                    existingTask.Title = taskItem.Title;
                    existingTask.Description = taskItem.Description;
                    existingTask.DueDate = taskItem.DueDate;

                    await this._taskItemService.UpdateTaskItemAsync(existingTask);
                }

                return this.RedirectToAction("Details", "TodoList", new { id = taskItem.TodoListId });
            }

            return this.View(taskItem);
        }

        public async Task<IActionResult> DeleteTask(int id)
        {
            var taskItem = await this._taskItemService.GetTaskItemByIdAsync(id);
            if (taskItem == null)
            {
                return this.NotFound();
            }

            return this.View(taskItem);
        }

        [HttpPost, ActionName("DeleteTask")]
        public async Task<IActionResult> DeleteTaskConfirmed(int id)
        {
            var taskItem = await this._taskItemService.GetTaskItemByIdAsync(id);
            await this._taskItemService.DeleteTaskItemAsync(id);
            return this.RedirectToAction("Details", "TodoList", new { id = taskItem.TodoListId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var taskItem = await this._taskItemService.GetTaskItemByIdAsync(id);
            if (taskItem == null)
            {
                return this.NotFound();
            }

            return this.View(taskItem);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                this.ViewData["Query"] = query;
                return this.View("Search", new List<TaskItem>());
            }

            var taskItems = await this._taskItemService.GetTaskItemsAsync();
            var searchResults = taskItems.Where(t => t.Title.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            this.ViewData["Query"] = query;
            return this.View("Search", searchResults);
        }
    }
}
