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
    private AddTodoItemViewModel _addTodoItemViewModel;

    public TodoItemsViewModel()
    {
        OpenPopupCommand = new DelegateCommand(OpenPopup);
        _addTodoItemViewModel = new AddTodoItemViewModel();
    }

    public AddTodoItemViewModel AddTodoItemViewModel
    {
        get => _addTodoItemViewModel;
        set => SetProperty(ref _addTodoItemViewModel, value);
    }

    public ObservableCollection<TodoItem> TodoItems { get; } = new();

    public DelegateCommand OpenPopupCommand { get; }

    private void RemoveTodoItem(TodoItem item)
    {
        TodoItems.Remove(item);
    }

    private async void OpenPopup()
    {
        var popup = new Window()
        {
            Content = new AddTodoItemView {DataContext=AddTodoItemViewModel},
            Width = 600,
            Height = 350,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
        };

        popup.Closed += AddItemPopupWindow_Closed;

        var mainWindow = Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.MainWindow : null;

        await popup.ShowDialog(mainWindow);

        if (_addTodoItemViewModel.IsValid)
        {
            TodoItems.Add(_addTodoItemViewModel.CreatedItem);
        }
    }

    private void AddItemPopupWindow_Closed(object sender, EventArgs e)
    {
        
    }
}
