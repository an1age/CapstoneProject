using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Models.Data;
using TodoListApp.WebApi.Models.Entities;

namespace TodoListApp.WebApi.Services
{
    public class TodoListDatabaseService : ITodoListDatabaseService
    {
        private readonly TodoListDbContext _context;

        public TodoListDatabaseService(TodoListDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<TodoList>> GetTodoListsAsync()
        {
            return await this._context.TodoLists.Include(t => t.TaskItems).ToListAsync();
        }

        public async Task<TodoList> GetTodoListByIdAsync(int id)
        {
            return await this._context.TodoLists.Include(t => t.TaskItems).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TodoList> AddTodoListAsync(TodoList todoList)
        {
            this._context.TodoLists.Add(todoList);
            await this._context.SaveChangesAsync();
            return todoList;
        }

        public async Task<TodoList> UpdateTodoListAsync(TodoList todoList)
        {
            this._context.TodoLists.Update(todoList);
            await this._context.SaveChangesAsync();
            return todoList;
        }

        public async Task<bool> DeleteTodoListAsync(int id)
        {
            var todoList = await this._context.TodoLists.FindAsync(id);
            if (todoList == null)
            {
                return false;
            }

            this._context.TodoLists.Remove(todoList);
            await this._context.SaveChangesAsync();
            return true;
        }
    }
}
