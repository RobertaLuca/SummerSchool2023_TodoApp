using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;
using DynamicData;
using System.Collections.ObjectModel;
using System.Text.Json;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.Views;

namespace TodoApp.ViewModels;

public sealed partial class TodoItemsViewModel : ViewModelBase
{
	private readonly NavigationService _navigationService;
	private readonly CurrentTodoService _currentTodoService;

	[ObservableProperty] private string? _chatResponse;
	[ObservableProperty] private string? _theoreticalInfo;
	[ObservableProperty] private bool? _backgroundTask;

	public TodoItemsViewModel(NavigationService navigationService, CurrentTodoService currentTodoService)
	{
		_navigationService = navigationService;
		_currentTodoService = currentTodoService;
		BackgroundTask = false;
	}

	[RelayCommand]
	private void DeleteItem(TodoItemViewModel? item)
	{
		if (item is not null)
		{
			TodoItems.Remove(item);
		}
	}

	// for filtering
	private List<TodoItemViewModel> _allTodoItems = new();

	public ObservableCollection<TodoItemViewModel> TodoItems { get; } = new();

	[RelayCommand]
	private async Task GenerateTasks(string topic)
	{
		// TODO: reflect
		// should the new redesign app still be able to generate a list of todos?
		// maybe would it be helpful to break down even further the given todos from the handout

		//BackgroundTask = true;
		//if (string.IsNullOrEmpty(topic))
		//{             
		//    return;
		//}
		//string fullInfo = Constants.Messages.GetTaskListMessage(topic, 5);
		//string response = await _chatBotService.GetResponse(fullInfo, ChatGPTService.DefaultModel);

		//var tasks = response.ExtractTasks();
		//foreach (var task in tasks)
		//{
		//    TodoItem todoItem = new ()
		//    {
		//        Title = task.Title,
		//        Description = task.Description,
		//        DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
		//    };
		//    _allTodoItems.Add(new TodoItemViewModel(todoItem));
		//    TodoItems.Add(new TodoItemViewModel(todoItem));
		//}
		//BackgroundTask = false;
	}

	[RelayCommand]
	private async Task OpenAddItemPopup()
	{
		AddTodoItemViewModel addTodoItemViewModel = new();
		
		var icon = new Uri("avares://TodoApp/Assets/avalonia-logo.ico");
		Window addItemPopup = new()
		{
			Content = new AddTodoItemView { DataContext = addTodoItemViewModel },
			Width = 600,
			Height = 350,
			WindowStartupLocation = WindowStartupLocation.CenterOwner,
			Icon = new WindowIcon(AssetLoader.Open(icon))
		};

		addTodoItemViewModel.ClosePopup += () => addItemPopup.Close();

		Window mainWindow = Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.MainWindow! : throw new Exception("null main window");
		await addItemPopup.ShowDialog(mainWindow);

		TodoItem? item = addTodoItemViewModel.CreatedItem;

		if (item is not null)
		{
			var task = new TodoItemViewModel(item, DeleteItemCommand, OpenTodoCommand);
			TodoItems.Add(task);
		}
	}

	[RelayCommand]
	public void GetNotDueItems()
	{
		_allTodoItems = TodoItems.OrderBy(x => x.DueDate).Where(x => x.DueDate <= DateOnly.FromDateTime(DateTime.Now)).ToList();

		TodoItems.RemoveMany(_allTodoItems);
	}

	[RelayCommand]
	private void GetAllItems()
	{
		TodoItems.AddRange(_allTodoItems);

		_allTodoItems.Clear();
	}

	[RelayCommand]
	private void OpenTodo(TodoItemViewModel vm)
	{
		TodoItem item = new()
		{ 
			Description = vm.Description, 
			DueDate = vm.DueDate, 
			Title = vm.Title 
		};

		_currentTodoService.CurrentTodo = item;
		_navigationService.Navigate<ChatViewModel>();
	}

	[RelayCommand]
	private void GoToOptions()
	{
		_navigationService.Navigate<OptionsViewModel>();
	}

	[RelayCommand]
	private async Task ImportTodos()
	{
		Window mainWindow = Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.MainWindow! : throw new Exception("null main window");
		var storage = mainWindow.StorageProvider;

		var result = await storage.OpenFilePickerAsync(new FilePickerOpenOptions()
		{
			Title = "Import todos",
			AllowMultiple = false,
			FileTypeFilter = new List<FilePickerFileType>()
			{
				new FilePickerFileType("json")
				{
					Patterns = new[] { "*.json" }
				}
			}
		});

		if (result.Count is 1)
		{
			string filePath = result[0].Path.LocalPath;

			Handout handout = JsonSerializer.Deserialize<Handout>(File.ReadAllText(filePath)) ?? throw new Exception("null handout");

			TheoreticalInfo = handout.TheoreticalInfo;

			foreach (var item in handout.Todos)
			{
				TodoItems.Add(new TodoItemViewModel(item, DeleteItemCommand, OpenTodoCommand));
			}
		}
	}
}