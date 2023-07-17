using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Collections.ObjectModel;
using TodoApp.Helper;
using TodoApp.Models;
using TodoApp.Views;

namespace TodoApp.ViewModels;

public sealed partial class TodoItemsViewModel : ViewModelBase
{
    private ObservableCollection<TodoItem> _todoItems = new ObservableCollection<TodoItem>() { 
        new TodoItem("Test", "desc", DateTime.Now),
        new TodoItem("Test", "desc", DateTime.Now),
        new TodoItem("Test", "desc", DateTime.Now),
        new TodoItem("Test", "desc", DateTime.Now),
        new TodoItem("Test", "desc", DateTime.Now),
        new TodoItem("Test", "desc", DateTime.Now),
        new TodoItem("Test", "desc", DateTime.Now),
        new TodoItem("Test", "desc", DateTime.Now),
        new TodoItem("Test", "desc", DateTime.Now),
        new TodoItem("Test", "desc", DateTime.Now),
        new TodoItem("Test", "desc", DateTime.Now),
        new TodoItem("Test", "desc", DateTime.Now),
        new TodoItem("Test", "desc", DateTime.Now),
        new TodoItem("Test", "desc", DateTime.Now),
        new TodoItem("Test", "desc", DateTime.Now),
    };

    private AddTodoItemViewModel _addTodoItemViewModel;

    public TodoItemsViewModel()
    {
        OpenPopupCommand = new DelegateCommand(OpenPopup);
    }

    public AddTodoItemViewModel AddTodoItemViewModel
    {
        get => _addTodoItemViewModel;
        set => SetProperty(ref _addTodoItemViewModel, value);
    }

    public ObservableCollection<TodoItem> TodoItems
    {
        get => _todoItems;
        set => SetProperty(ref _todoItems, value);
    }

    public DelegateCommand OpenPopupCommand { get; }

    private void AddTodoItem(TodoItem item)
    {
        TodoItems.Add(item);
    }

    private void RemoveTodoItem(TodoItem item)
    {
        TodoItems.Remove(item);
    }

    private void OpenPopup()
    {
        var popup = new Window()
        {
            Content = new AddTodoItemView {},
            Width = 600,
            Height = 350,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
        };

        var mainWindow = Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.MainWindow : null;

        popup.ShowDialog(mainWindow);
    }
}
