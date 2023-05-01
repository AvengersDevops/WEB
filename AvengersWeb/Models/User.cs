using Newtonsoft.Json.Linq;

namespace AvengersWeb.Models;

public class User
{
    public int? Id;
    public string? Name;
    public string? Email;
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
            id: json["id"]?.ToObject<int?>(),
            name: json["name"]?.ToObject<string?>(),
            email: json["email"]?.ToObject<string?>(),
            password: json["password"]?.ToObject<string?>()
        );
    }

    public Dictionary<string,dynamic> ToJson()
    {
        return new Dictionary<string,dynamic>
        {
            {"id", Id!},
            {"name", Name!},
            {"email", Email!},
            {"password", Password!}
        };
    }
}
