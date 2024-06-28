using TodoListApp.WebApi.Models.Entities;

namespace TodoListApp.WebApp.Services
{
    public interface ITaskItemWebApiService
    {
        Task<IEnumerable<TaskItem>> GetTaskItemsAsync();

        Task<TaskItem> GetTaskItemByIdAsync(int id);

        Task<TaskItem> CreateTaskItemAsync(TaskItem taskItem);

        Task UpdateTaskItemAsync(TaskItem taskItem);

        Task DeleteTaskItemAsync(int id);
    }
}
