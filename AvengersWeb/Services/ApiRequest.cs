using AvengersWeb.Models;
using Newtonsoft.Json.Linq;

namespace AvengersWeb.Services;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Task = Models.Task;

public class ApiRequest
{
    private HttpClient _client = new();
    private const string URL = "https://avengerstodo.azurewebsites.net/";

    public async Task<T> Invoke<T>(Dictionary<string, dynamic> body, string path)
    {
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(body);

        var request = new HttpRequestMessage(HttpMethod.Post, URL + path);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error calling {path}: {response.StatusCode}");
        }

        var stringResponse = await response.Content.ReadAsStringAsync();
        var jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(stringResponse);
        
        if (jsonResponse == null)
        {
            throw new Exception($"Error calling {path}: {stringResponse}");
        }

        var status = jsonResponse["status"].ToString();
        var message = jsonResponse["message"].ToString();
        var data = jsonResponse["data"];

        if (status == null || message == null)
        {
            throw new Exception($"Error calling {path}: {stringResponse}");
        }

        if (status != "success")
        {
            throw new Exception($"Call to {path} failed: {message}");
        }

        if (data == null)
        {
            throw new Exception($"There is no data in the response from {path}");
        }

        Func<JToken, dynamic> fromJson = typeof(T).Name switch
        {
            "User" => User.FromJson,
            "Task" => Task.FromJson,
            // list of tasks
            "List`1" => Task.FromJsonList, 
            _ => throw new Exception($"Unknown type {typeof(T).Name}")
        };

        T converted = (T)fromJson(data);

        if (converted == null)
        {
            throw new Exception($"Error converting data from {path} to {typeof(T)}");
        }

        return converted;
    }
}
