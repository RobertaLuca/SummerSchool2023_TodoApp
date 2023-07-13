using System;
using TodoApp.Models;

namespace TodoApp.ViewModels;

public partial class TodoItemsViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public TodoItem Item => new TodoItem("test", "desc", DateOnly.FromDateTime(DateTime.Now));
}
