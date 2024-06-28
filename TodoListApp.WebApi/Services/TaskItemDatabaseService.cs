using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Models.Data;
using TodoListApp.WebApi.Models.Entities;

namespace TodoListApp.WebApi.Services
{
    public class TaskItemDatabaseService : ITaskItemDatabaseService
    {
        private readonly TodoListDbContext _context;

        public TaskItemDatabaseService(TodoListDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetTaskItemsAsync()
        {
            return await this._context.TaskItems.ToListAsync();
        }

        public async Task<TaskItem> GetTaskItemByIdAsync(int id)
        {
            return await this._context.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaskItem> AddTaskItemAsync(TaskItem taskItem)
        {
            this._context.TaskItems.Add(taskItem);
            await this._context.SaveChangesAsync();
            return taskItem;
        }

        public async Task<TaskItem> UpdateTaskItemAsync(TaskItem taskItem)
        {
            this._context.TaskItems.Update(taskItem);
            await this._context.SaveChangesAsync();
            return taskItem;
        }

        public async Task<bool> DeleteTaskItemAsync(int id)
        {
            var taskItem = await this._context.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                return false;
            }

            this._context.TaskItems.Remove(taskItem);
            await this._context.SaveChangesAsync();
            return true;
        }
    }
}
