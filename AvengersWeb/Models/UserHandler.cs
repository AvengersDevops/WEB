namespace AvengersWeb.Models;

public class UserHandler
{
    public static User User { get; set; } = new();

    public static List<Task> Tasks { get; set; } = new();
}