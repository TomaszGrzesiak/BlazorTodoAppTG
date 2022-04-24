using System.Text;
using System.Text.Json;
using DomainOrEntities.ContractsOrInterfaces;
using DomainOrEntities.Models;

namespace HttpServices;

public class TodoHttpClient : ITodoHomeOrDaoOrRep
{
    public async Task<ICollection<Todo>> GetAsync()
    {
        using HttpClient httpClient = new();
        HttpResponseMessage responseMessage = await httpClient.GetAsync("https://localhost:7228/Todos");
        string content = await responseMessage.Content.ReadAsStringAsync();

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {responseMessage.StatusCode}, {content}");
        }

        ICollection<Todo> todos = JsonSerializer.Deserialize<ICollection<Todo>>(content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })!;
        return todos;
    }

    public async Task<Todo> GetById(int id)
    {
        using HttpClient httpClient = new();
        HttpResponseMessage responseMessage = await httpClient.GetAsync($"https://localhost:7228/Todos/{id}");
        string content = await responseMessage.Content.ReadAsStringAsync();

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {responseMessage.StatusCode}, {content}");
        }

        Todo todo = JsonSerializer.Deserialize<Todo>(content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })!;
        return todo;
    }

    public async Task<Todo> AddAsync(Todo todo)
    {
        using HttpClient httpClient = new();
        string todoAsJson = JsonSerializer.Serialize(todo);
        StringContent content = new(todoAsJson, Encoding.UTF8, "application/json");
        // string test = await content.ReadAsStringAsync();
        // Console.WriteLine(test);
        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("https://localhost:7228/TodosPostBlabla/", content);
        string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
        

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {httpResponseMessage.StatusCode}, {responseContent}");
        }

        Todo returned = JsonSerializer.Deserialize<Todo>(responseContent, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })!;
            
            return returned;
    }

    public async Task DeleteAsync(int id)
    {
        using HttpClient httpClient = new();
        HttpResponseMessage responseMessage = await httpClient.DeleteAsync($"https://localhost:7228/Todos/{id}");
        string content = await responseMessage.Content.ReadAsStringAsync();

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {responseMessage.StatusCode}, {content}");
        }
    }

    public async Task UpdateAsync(Todo todo)
    {
        using HttpClient client = new();
        string todoAsJson = JsonSerializer.Serialize(todo);
        StringContent content = new(todoAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PatchAsync("https://localhost:7228/todos", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
    }
}