namespace TodoApp.Models;

public interface ITodoItem
{
    string Title { get; }
    string Description { get; }
    DateOnly DueDate { get; }
    bool IsDone { get; }
}
