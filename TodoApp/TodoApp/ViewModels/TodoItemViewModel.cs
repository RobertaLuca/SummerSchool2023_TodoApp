using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using TodoApp.Models;

namespace TodoApp.ViewModels;

public sealed partial class TodoItemViewModel : ViewModelBase
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _description;
    [ObservableProperty] private DateOnly _dueDate;
    [ObservableProperty] private bool _isDone;

    public TodoItemViewModel(TodoItem item, ICommand deleteCommand, ICommand openCommand)
    {
        // TODO: reflect
        // wouldn't it be better to have a property of type "TodoItem", this can simplify the scenario in which you need to retrieve an TodoItem object from the VM
        // but in the same time may cause problems if the properties of the TodoItem are not observable properties and they can change without setting them from the gui
        _title = item.Title;
        _description = item.Description;
        _dueDate = item.DueDate;
        _isDone = item.IsDone;

		DeleteCommand = deleteCommand;
		OpenCommand = openCommand;
	}

	public ICommand DeleteCommand { get; }
	public ICommand OpenCommand { get; }
}
