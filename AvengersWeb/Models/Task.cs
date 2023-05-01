using System.Collections;
using Newtonsoft.Json.Linq;

namespace AvengersWeb.Models;

using System;

public class Task
{
    public int? Id;
    public int? UserId;
    public string? Title;
    public string? Description;
    public DateTime? DueDate;
    public bool? Done;

    public Task(int? id = null, int? userId = null, string? title = null, string? description = null, DateTime? dueDate = null, bool? done = null)
    {
        Id = id;
        UserId = userId;
        Title = title;
        Description = description;
        DueDate = dueDate;
        Done = done;
    }

    public static Task FromJson(JToken json)
    {
        return new Task(
            id: json["id"]?.ToObject<int?>(),
            userId: json["userId"]?.ToObject<int?>(),
            title: json["title"]?.ToObject<string?>(),
            description: json["description"]?.ToObject<string?>(),
            dueDate: DateTime.Parse(json["dueDate"]?.ToObject<string?>() ?? ""),
            done: json["done"]?.ToObject<List<dynamic>?>()?.ToArray()[0]
        );
    }

    public static List<Task> FromJsonList(JToken json)
    {
        return json.Select(FromJson).ToList();
    }

    public Dictionary<string, dynamic> ToJson()
    {
        return new Dictionary<string, dynamic>
        {
            {"id", Id!},
            {"userId", UserId!},
            {"title", Title!},
            {"description", Description!},
            {"dueDate", DueDate?.ToString("yyyy-MM-ddTHH:mm:ss.fffK")!},
            {"done", Done!}
        }; 
    }

}
