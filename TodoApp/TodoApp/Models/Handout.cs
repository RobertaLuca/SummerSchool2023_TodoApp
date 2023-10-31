namespace TodoApp.Models;

public sealed class Handout
{
	public required string TheoreticalInfo { get; set; }

	public required List<TodoItem> Todos { get; set; }
}
