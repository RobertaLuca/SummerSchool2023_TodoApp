using System;
using TodoApp.Models;

namespace TodoApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public TodoItem Item => new TodoItem("test", "desc", DateOnly.FromDateTime(DateTime.Now));
}
