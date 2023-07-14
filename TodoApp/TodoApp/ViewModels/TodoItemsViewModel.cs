using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TodoApp.Models;
using Material.Icons;

namespace TodoApp.ViewModels;

public sealed partial class TodoItemsViewModel : ViewModelBase
{
    private ObservableCollection<TodoItem> _todoItems = new ObservableCollection<TodoItem>() { 
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
        new TodoItem("Test", "desc", DateOnly.FromDateTime(DateTime.Now)),
    };

    public string Greeting => "Welcome to Avalonia!";

    public TodoItem Item => new TodoItem("test", "desc", DateOnly.FromDateTime(DateTime.Now));

    public ObservableCollection<TodoItem> TodoItems
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
