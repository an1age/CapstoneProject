using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TodoListApp.WebApi.Models.Entities;
using TodoListApp.WebApp.Models;

namespace TodoListApp.WebApp.Services
{
    public class TodoListWebApiService : ITodoListWebApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _baseUrl;

        public TodoListWebApiService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            this._httpClient = httpClient;
            this._baseUrl = apiSettings.Value.BaseUrl;
        }

        public async Task<IEnumerable<TodoList>> GetTodoListsAsync()
        {
            var response = await this._httpClient.GetAsync($"{this._baseUrl}TodoList");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TodoList>>(content);
        }

        public async Task<TodoList> GetTodoListByIdAsync(int id)
        {
            var response = await this._httpClient.GetAsync($"{this._baseUrl}TodoList/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoList>(content);
        }

        public async Task<TodoList> CreateTodoListAsync(TodoList todoList)
        {
            var content = new StringContent(JsonConvert.SerializeObject(todoList), System.Text.Encoding.UTF8, "application/json");
            var response = await this._httpClient.PostAsync($"{this._baseUrl}TodoList", content);
            response.EnsureSuccessStatusCode();
            var resultContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoList>(resultContent);
        }

        public async Task UpdateTodoListAsync(TodoList todoList)
        {
            var content = new StringContent(JsonConvert.SerializeObject(todoList), System.Text.Encoding.UTF8, "application/json");
            var response = await this._httpClient.PutAsync($"{this._baseUrl}TodoList/{todoList.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTodoListAsync(int id)
        {
            var response = await this._httpClient.DeleteAsync($"{this._baseUrl}TodoList/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
