using AvengersWeb.Models;
using AvengersWeb.Repositories;
using Task = System.Threading.Tasks.Task;

namespace Tests;

public class UserTests
{
    private readonly UserRepository userRepository = new();

    [Test]
    public async Task Read()
    {
        var user = await userRepository.Read(1);
        Assert.That(user.Id, Is.Not.Null);
    }

    [Test]
    public async Task CreateDelete()
    {
        var user1 = await userRepository.Create(
            new User(name: "TestName", email: "TestEmail", password: "TestPassword"));
        Assert.That(user1.Id, Is.Not.Null);

        var user2 = await userRepository.Delete(user1.Id.Value);
        Assert.That(user2.Id, Is.Not.Null);
    }

    [Test]
    public async Task Update()
    {
        var user1 = await userRepository.Create(
            new User(name: "TestName", email: "TestEmail", password: "TestPassword"));
        Assert.That(user1.Id, Is.Not.Null);

        var user2 = await userRepository.Update(new User(user1.Id, "TestName2", "TestEmail2", "TestPassword2"));
        Assert.That(user2.Id, Is.Not.Null);

        var user3 = await userRepository.Delete(user2.Id.Value);
        Assert.That(user3.Id, Is.Not.Null);
    }

    [Test]
    public async Task Login()
    {
        var user = await userRepository.Login("Testemail", "Testpassword");
        Assert.That(user.Id, Is.Not.Null);
    }
}