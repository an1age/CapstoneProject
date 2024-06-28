using TodoListApp.WebApi.Models.Entities;

namespace TodoListApp.WebApi.Services
{
    public interface ITodoListDatabaseService
    {
        Task<IEnumerable<TodoList>> GetTodoListsAsync();

        Task<TodoList> GetTodoListByIdAsync(int id);

        Task<TodoList> AddTodoListAsync(TodoList todoList);

        Task<TodoList> UpdateTodoListAsync(TodoList todoList);

        Task<bool> DeleteTodoListAsync(int id);
    }
}
