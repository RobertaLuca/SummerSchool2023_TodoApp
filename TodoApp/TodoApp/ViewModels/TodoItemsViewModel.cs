using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;
using DynamicData;
using System.Collections.ObjectModel;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.Views;

namespace TodoApp.ViewModels;

public sealed partial class TodoItemsViewModel : ViewModelBase
{
	private readonly IChatBotService _chatBotService;
	private readonly NavigationService _navigationService;
	private readonly CurrentTodoService _currentTodoService;
	private readonly PageService _pageService;

	[ObservableProperty] private string? _chatResponse;
	[ObservableProperty] private bool? _backgroundTask;

	public TodoItemsViewModel(IChatBotService chatBotService, NavigationService navigationService, CurrentTodoService currentTodoService, PageService pageService)
	{
		_chatBotService = chatBotService;
		_navigationService = navigationService;
		_currentTodoService = currentTodoService;
		_pageService = pageService;
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
	private async Task CalculateMass(TodoItemViewModel todoItem)
	{
		BackgroundTask = true;

		if (todoItem.IsDone)
		{
			ChatResponse = "Congratulations on the completed task!";
			return;
		}

		ChatResponse = await _chatBotService.AskAdvice(todoItem.Title, todoItem.Description);
		BackgroundTask = false;
	}

	[RelayCommand]
	private async Task GenerateTasks(string topic)
	{
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

		var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
		var icon = assets?.Open(new Uri("avares://TodoApp/Assets/avalonia-logo.ico"));
		Window addItemPopup = new()
		{
			Content = new AddTodoItemView { DataContext = addTodoItemViewModel },
			Width = 600,
			Height = 350,
			WindowStartupLocation = WindowStartupLocation.CenterOwner,
			Icon = new WindowIcon(icon)
		};

		addTodoItemViewModel.ClosePopup += () => addItemPopup.Close();

		Window? mainWindow = Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.MainWindow : null;
		await addItemPopup.ShowDialog(mainWindow);

		if (addTodoItemViewModel.IsValid)
		{
			var task = new TodoItemViewModel(addTodoItemViewModel.CreatedItem!, DeleteItemCommand, OpenTodoCommand);
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
	public void GetAllItems()
	{
		TodoItems.AddRange(_allTodoItems);

		_allTodoItems.Clear();
	}

	[RelayCommand]
	public void OpenTodo(TodoItemViewModel vm)
	{
		TodoItem item = new()
		{ 
			Description = vm.Description, 
			DueDate = vm.DueDate, 
			Title = vm.Title 
		};

		_currentTodoService.CurrentTodo = item;
		_navigationService.CurrentPageData = _pageService.Pages[typeof(ChatViewModel)];
	}
}