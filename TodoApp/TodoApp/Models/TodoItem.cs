namespace TodoApp.Models;

public class TodoItem : ITodoItem
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateOnly DueDate { get; set; }
    public bool IsDone { get; set; }
}
