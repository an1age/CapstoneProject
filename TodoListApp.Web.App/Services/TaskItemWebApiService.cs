using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TodoListApp.WebApi.Models.Entities;
using TodoListApp.WebApp.Models;

namespace TodoListApp.WebApp.Services
{
    public class TaskItemWebApiService : ITaskItemWebApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _baseUrl;

        public TaskItemWebApiService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            this._httpClient = httpClient;
            this._baseUrl = apiSettings.Value.BaseUrl;
        }

        public async Task<IEnumerable<TaskItem>> GetTaskItemsAsync()
        {
            var response = await this._httpClient.GetAsync($"{this._baseUrl}TaskItem");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TaskItem>>(content);
        }

        public async Task<TaskItem> GetTaskItemByIdAsync(int id)
        {
            var response = await this._httpClient.GetAsync($"{this._baseUrl}TaskItem/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TaskItem>(content);
        }

        public async Task<TaskItem> CreateTaskItemAsync(TaskItem taskItem)
        {
            var content = new StringContent(JsonConvert.SerializeObject(taskItem), System.Text.Encoding.UTF8, "application/json");
            var response = await this._httpClient.PostAsync($"{this._baseUrl}TaskItem", content);
            response.EnsureSuccessStatusCode();
            var resultContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TaskItem>(resultContent);
        }

        public async Task UpdateTaskItemAsync(TaskItem taskItem)
        {
            var content = new StringContent(JsonConvert.SerializeObject(taskItem), System.Text.Encoding.UTF8, "application/json");
            var response = await this._httpClient.PutAsync($"{this._baseUrl}TaskItem/{taskItem.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTaskItemAsync(int id)
        {
            var response = await this._httpClient.DeleteAsync($"{this._baseUrl}TaskItem/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
