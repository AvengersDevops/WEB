using Newtonsoft.Json.Linq;

namespace AvengersWeb.Models;

public class User
{
    public string? Email;
    public int? Id;
    public string? Name;
    public string? Password;

    public User(int? id = null, string? name = null, string? email = null, string? password = null)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }

    public static User FromJson(JToken json)
    {
        return new User(
            json["id"]?.ToObject<int?>(),
            json["name"]?.ToObject<string?>(),
            json["email"]?.ToObject<string?>(),
            json["password"]?.ToObject<string?>()
        );
    }

    public Dictionary<string, dynamic> ToJson()
    {
        return new Dictionary<string, dynamic>
        {
            { "id", Id! },
            { "name", Name! },
            { "email", Email! },
            { "password", Password! }
        };
    }
}