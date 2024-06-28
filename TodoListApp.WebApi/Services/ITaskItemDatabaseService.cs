using TodoListApp.WebApi.Models.Entities;

namespace TodoListApp.WebApi.Services
{
    public interface ITaskItemDatabaseService
    {
        Task<IEnumerable<TaskItem>> GetTaskItemsAsync();

        Task<TaskItem> GetTaskItemByIdAsync(int id);

        Task<TaskItem> AddTaskItemAsync(TaskItem taskItem);

        Task<TaskItem> UpdateTaskItemAsync(TaskItem taskItem);

        Task<bool> DeleteTaskItemAsync(int id);
    }
}
