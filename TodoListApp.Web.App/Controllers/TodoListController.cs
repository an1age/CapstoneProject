using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Models.Entities;
using TodoListApp.WebApp.Services;

namespace TodoListApp.WebApp.Controllers
{
    public class TodoListController : Controller
    {
        private readonly ITodoListWebApiService _todoListService;

        public TodoListController(ITodoListWebApiService todoListService)
        {
            this._todoListService = todoListService;
        }

        public async Task<IActionResult> Index()
        {
            var todoLists = await this._todoListService.GetTodoListsAsync();
            return this.View(todoLists);
        }

        public async Task<IActionResult> Details(int id)
        {
            var todoList = await this._todoListService.GetTodoListByIdAsync(id);
            if (todoList == null)
            {
                return this.NotFound();
            }

            return this.View(todoList);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoList todoList)
        {
            if (this.ModelState.IsValid)
            {
                todoList.TaskItems = new List<TaskItem>();
                await this._todoListService.CreateTodoListAsync(todoList);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(todoList);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var todoList = await this._todoListService.GetTodoListByIdAsync(id);
            if (todoList == null)
            {
                return this.NotFound();
            }

            return this.View(todoList);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TodoList todoList)
        {
            if (this.ModelState.IsValid)
            {
                await this._todoListService.UpdateTodoListAsync(todoList);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(todoList);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var todoList = await this._todoListService.GetTodoListByIdAsync(id);
            if (todoList == null)
            {
                return this.NotFound();
            }

            return this.View(todoList);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this._todoListService.DeleteTodoListAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
