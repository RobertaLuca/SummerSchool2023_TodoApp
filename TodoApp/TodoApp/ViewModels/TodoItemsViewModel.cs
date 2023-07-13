using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TodoApp.Models;

namespace TodoApp.ViewModels;

public sealed partial class TodoItemsViewModel : ViewModelBase
{
    private IList<TodoItem> _todoItems = new List<TodoItem>();

    public string Greeting => "Welcome to Avalonia!";

    public TodoItem Item => new TodoItem("test", "desc", DateOnly.FromDateTime(DateTime.Now));

    public IList<TodoItem> TodoItems
    {
        get => _todoItems;
        set => SetProperty(ref _todoItems, value);
    }

    private void AddTodoItem(TodoItem item)
    {
        TodoItems.Add(item);
    }

    private void RemoveTodoItem(TodoItem item)
    {
        TodoItems.Remove(item);
    }
}
