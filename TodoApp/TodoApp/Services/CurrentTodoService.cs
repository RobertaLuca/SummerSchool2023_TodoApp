using TodoApp.Models;

namespace TodoApp.Services;

public sealed class CurrentTodoService
{
	public TodoItem? CurrentTodo { get; set; }
}
