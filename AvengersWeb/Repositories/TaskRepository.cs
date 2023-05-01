using AvengersWeb.Services;

namespace AvengersWeb.Repositories;

using System.Collections.Generic;
using Task = Models.Task;

public class TaskRepository : ApiRequest
{
    private const string PATH = "task";

    public async Task<Task> Read(int id)
    {
        var body = new Dictionary<string, dynamic>
        {
            {"id", id}
        };
        return await Invoke<Task>(body, $"{PATH}/read");
    }

    public async Task<List<Task>> ReadAll(int userId)
    {
        var body = new Dictionary<string, dynamic>
        {
            {"userId", userId}
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
            {"id", id}
        };
        return await Invoke<Task>(body, $"{PATH}/delete");
    }
}
