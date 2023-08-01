using CommunityToolkit.Mvvm.ComponentModel;
using TodoApp.Helper;
using TodoApp.Models;

namespace TodoApp.ViewModels;

public sealed partial class AddTodoItemViewModel : ViewModelBase
{
    [ObservableProperty] private string _title = string.Empty;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private DateTimeOffset? _dueDate;
    [ObservableProperty] private bool _isValid = false;
    [ObservableProperty] private TodoItem? _createdItem;

    public AddTodoItemViewModel()
    {
        SaveItemCommand = new DelegateCommand(SaveItem);
    }

    public DelegateCommand SaveItemCommand { get; }

    public event Action? ClosePopup;

    private void SaveItem()
    {
        if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Description) || DueDate is null)
        {
            IsValid = false;
        }
        else
        {
            IsValid = true;
            CreatedItem = new TodoItem()
            {
                Title = Title,
                Description = Description,
                DueDate = DateOnly.FromDateTime(DueDate.Value.Date)
            };
        }

        ClosePopup?.Invoke();
    }
}
