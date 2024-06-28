using TodoListApp.WebApi.Models.Entities;

namespace TodoListApp.WebApp.Services
{
    public interface ITodoListWebApiService
    {
        Task<IEnumerable<TodoList>> GetTodoListsAsync();

        Task<TodoList> GetTodoListByIdAsync(int id);

        Task<TodoList> CreateTodoListAsync(TodoList todoList);

        Task UpdateTodoListAsync(TodoList todoList);

        Task DeleteTodoListAsync(int id);
    }
}
