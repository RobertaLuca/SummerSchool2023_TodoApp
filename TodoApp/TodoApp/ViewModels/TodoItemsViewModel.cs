using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TodoApp.Models;

namespace TodoApp.ViewModels;

public sealed partial class TodoItemsViewModel : ViewModelBase, INotifyPropertyChanged
{
    private IList<TodoItem> _todoItems = new List<TodoItem>();

    public string Greeting => "Welcome to Avalonia!";

    public TodoItem Item => new TodoItem("test", "desc", DateOnly.FromDateTime(DateTime.Now));

    public IList<TodoItem> TodoItems
    {
        get
        {
            return _todoItems;
        }

        set
        {
            _todoItems = value;
            NotifyPropertyChanged();
        }
    }

    private void AddTodoItem(TodoItem item)
    {
        TodoItems.Add(item);
    }

    private void RemoveTodoItem(TodoItem item)
    {
        TodoItems.Remove(item);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
