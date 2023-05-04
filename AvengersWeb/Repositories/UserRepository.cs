using AvengersWeb.Models;
using AvengersWeb.Services;

namespace AvengersWeb.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRepository : ApiRequest
{
    private const string PATH = "user";

    public async Task<User> Read(int id)
    {
        var body = new Dictionary<string, dynamic>
        {
            {"id", id}
        };
        return await Invoke<User>(body, $"{PATH}/read");
    }

    public async Task<User> Create(User user)
    {
        var body = user.ToJson();
        return await Invoke<User>(body, $"{PATH}/create");
    }

    public async Task<User> Update(User user)
    {
        var body = user.ToJson();
        return await Invoke<User>(body, $"{PATH}/update");
    }

    public async Task<User> Delete(int id)
    {
        var body = new Dictionary<string, dynamic>
        {
            {"id", id}
        };
        return await Invoke<User>(body, $"{PATH}/delete");
    }

    public async Task<User> Login(string email, string password)
    {
        var body = new Dictionary<string, dynamic>
        {
            {"email", email},
            {"password", password}
        };
        return await Invoke<User>(body, $"{PATH}/login");
    }
}
