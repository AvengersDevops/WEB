using System.Text;
using AvengersWeb.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Task = AvengersWeb.Models.Task;

namespace AvengersWeb.Services;

public class ApiService
{
    private const string URL = "http://128.140.9.68/";
    private readonly HttpClient _client = new();

    public async Task<T> Invoke<T>(Dictionary<string, dynamic> body, string path)
    {
        var json = JsonConvert.SerializeObject(body);

        var request = new HttpRequestMessage(HttpMethod.Post, URL + path);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.SendAsync(request);

        if (!response.IsSuccessStatusCode) throw new Exception($"Error calling {path}: {response.StatusCode}");

        var stringResponse = await response.Content.ReadAsStringAsync();
        var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(stringResponse);

        if (jsonResponse == null) throw new Exception($"Error calling {path}: {stringResponse}");

        var status = jsonResponse["status"].ToString();
        var message = jsonResponse["message"].ToString();
        var data = jsonResponse["data"];

        if (status == null || message == null) throw new Exception($"Error calling {path}: {stringResponse}");

        if (status != "success") throw new Exception($"Call to {path} failed: {message}");

        if (data == null) throw new Exception($"There is no data in the response from {path}");

        Func<JToken, dynamic> fromJson = typeof(T).Name switch
        {
            "User" => User.FromJson,
            "Task" => Task.FromJson,
            // list of tasks
            "List`1" => Task.FromJsonList,
            _ => throw new Exception($"Unknown type {typeof(T).Name}")
        };

        var converted = (T)fromJson(data);

        if (converted == null) throw new Exception($"Error converting data from {path} to {typeof(T)}");

        return converted;
    }
}
