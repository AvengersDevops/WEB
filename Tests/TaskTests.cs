using AvengersWeb.Repositories;
using Task = AvengersWeb.Models.Task;

namespace Tests;

public class TaskTests
{
    TaskRepository taskRepository = new();
    
    [Test]
    public async System.Threading.Tasks.Task Read()
    {
        Task task = await taskRepository.Read(2);
        Assert.That(task.Id, Is.Not.Null);
    }
    
    [Test]
    public async System.Threading.Tasks.Task ReadAll()
    {
        List<Task> tasks = await taskRepository.ReadAll(1);
        Assert.That(tasks, Is.Not.Empty);
    }
    
    [Test]
    public async System.Threading.Tasks.Task CreateDelete()
    {
        Task task1 = await taskRepository.Create(new Task(userId: 1, title: "TestName", description: "TestDescription", dueDate: DateTime.Now, done: false));
        Assert.That(task1.Id, Is.Not.Null);
        
        Task task2 = await taskRepository.Delete(task1.Id.Value);
        Assert.That(task2.Id, Is.Not.Null);
    }
    
    [Test]
    public async System.Threading.Tasks.Task Update()
    {
        Task task1 = await taskRepository.Create(new Task(userId: 1, title: "TestName", description: "TestDescription", dueDate: DateTime.Now, done: false));
        Assert.That(task1.Id, Is.Not.Null);
        
        Task task2 = await taskRepository.Update(new Task(id: task1.Id, userId: 1, title: "TestName2", description: "TestDescription2", dueDate: DateTime.Now, done: false));
        Assert.That(task2.Id, Is.Not.Null);
        
        Task task3 = await taskRepository.Delete(task2.Id.Value);
        Assert.That(task3.Id, Is.Not.Null);
    }
}