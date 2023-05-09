using AvengersWeb.Repositories;

namespace Tests;

public class TaskTests
{
    private readonly TaskRepository taskRepository = new();

    [Test]
    public async Task Read()
    {
        var task = await taskRepository.Read(2);
        Assert.That(task.Id, Is.Not.Null);
    }

    [Test]
    public async Task ReadAll()
    {
        var tasks = await taskRepository.ReadAll(1);
        Assert.That(tasks, Is.Not.Empty);
    }

    [Test]
    public async Task CreateDelete()
    {
        var task1 = await taskRepository.Create(new AvengersWeb.Models.Task(userId: 1, title: "TestName",
            description: "TestDescription", dueDate: DateTime.Now, done: false));
        Assert.That(task1.Id, Is.Not.Null);

        var task2 = await taskRepository.Delete(task1.Id.Value);
        Assert.That(task2.Id, Is.Not.Null);
    }

    [Test]
    public async Task Update()
    {
        var task1 = await taskRepository.Create(new AvengersWeb.Models.Task(userId: 1, title: "TestName",
            description: "TestDescription", dueDate: DateTime.Now, done: false));
        Assert.That(task1.Id, Is.Not.Null);

        var task2 = await taskRepository.Update(new AvengersWeb.Models.Task(task1.Id, 1, "TestName2",
            "TestDescription2", DateTime.Now, false));
        Assert.That(task2.Id, Is.Not.Null);

        var task3 = await taskRepository.Delete(task2.Id.Value);
        Assert.That(task3.Id, Is.Not.Null);
    }
}