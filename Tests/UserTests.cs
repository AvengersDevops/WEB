using AvengersWeb.Repositories;
using User = AvengersWeb.Models.User;

namespace Tests;

public class UserTests
{
    UserRepository userRepository = new();

    [Test]
    public async Task Read()
    {
        User user = await userRepository.Read(1);
        Assert.That(user.Id, Is.Not.Null);
    }

    [Test]
    public async Task CreateDelete()
    {
        User user1 = await userRepository.Create(new User(name: "TestName", email: "TestEmail", password: "TestPassword"));
        Assert.That(user1.Id, Is.Not.Null);
        
        User user2 = await userRepository.Delete(user1.Id.Value);
        Assert.That(user2.Id, Is.Not.Null);
    }
    
    [Test]
    public async Task Update()
    {
        User user1 = await userRepository.Create(new User(name: "TestName", email: "TestEmail", password: "TestPassword"));
        Assert.That(user1.Id, Is.Not.Null);
        
        User user2 = await userRepository.Update(new User(id: user1.Id, name: "TestName2", email: "TestEmail2", password: "TestPassword2"));
        Assert.That(user2.Id, Is.Not.Null);
        
        User user3 = await userRepository.Delete(user2.Id.Value);
        Assert.That(user3.Id, Is.Not.Null);
    }

    [Test]
    public async Task Login()
    {
        User user = await userRepository.Login("Testemail", "Testpassword");
        Assert.That(user.Id, Is.Not.Null);
    }
}