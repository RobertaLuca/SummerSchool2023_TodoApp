using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoApp.Models;

namespace TodoApp.ViewModels;

public sealed partial class AddTodoItemViewModel : ViewModelBase
{
	[ObservableProperty] private string _title = string.Empty;
	[ObservableProperty] private string _description = string.Empty;
	[ObservableProperty] private DateTimeOffset? _dueDate;
	[ObservableProperty] private TodoItem? _createdItem;

	public event Action? ClosePopup;

	[RelayCommand]
	private void SaveItem()
	{
		if (!(string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Description) || DueDate is null))
		{
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
