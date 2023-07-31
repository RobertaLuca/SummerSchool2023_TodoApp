using CommunityToolkit.Mvvm.ComponentModel;
using TodoApp.Models;

namespace TodoApp.ViewModels;

public sealed partial class TodoItemViewModel : ViewModelBase
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _description;
    [ObservableProperty] private DateOnly _dueDate;
    [ObservableProperty] private bool _isDone;

    public TodoItemViewModel(TodoItem item)
    {
        _title = item.Title;
        _description = item.Description;
        _dueDate = item.DueDate;
        _isDone = item.IsDone;
    }
}
