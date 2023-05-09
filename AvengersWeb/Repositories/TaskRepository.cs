using AvengersWeb.Services;
using Task = AvengersWeb.Models.Task;

namespace AvengersWeb.Repositories;

public class TaskRepository : ApiService
{
    private const string PATH = "task";

    public async Task<Task> Read(int id)
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", id }
        };
        return await Invoke<Task>(body, $"{PATH}/read");
    }

    public async Task<List<Task>> ReadAll(int userId)
    {
        var body = new Dictionary<string, dynamic>
        {
            { "userId", userId }
        };
        return await Invoke<List<Task>>(body, $"{PATH}/readAll");
    }

    public async Task<Task> Create(Task task)
    {
        var body = task.ToJson();
        return await Invoke<Task>(body, $"{PATH}/create");
    }

    public async Task<Task> Update(Task task)
    {
        var body = task.ToJson();
        return await Invoke<Task>(body, $"{PATH}/update");
    }

    public async Task<Task> Delete(int id)
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", id }
        };
        return await Invoke<Task>(body, $"{PATH}/delete");
    }
}