using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TodoApp.Helpers;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.Views;

namespace TodoApp.ViewModels;

public sealed partial class TodoItemsViewModel : ViewModelBase
{
    private readonly IChatBotService _chatBotService;

    public TodoItemsViewModel()
    {
        _chatBotService = new ChatGPTService(Utils.ReadSetting("OPENAI_API_KEY"));

        BackgroundTask = false;
    }

    [RelayCommand]
    private void DeleteItem(TodoItemViewModel? item)
    {
        if (item is not null)
        {
            _allTodoItems.Remove(item);
            TodoItems.Remove(item);
        }
    }

    [ObservableProperty]
    private string? _chatResponse;

    [ObservableProperty]
    private bool? _backgroundTask;

    // initial implementation
    //public ObservableCollection<TodoItem> TodoItems { get; } = new();

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

        var fullInfo = $"{todoItem.Title} ({todoItem.Description})";
        fullInfo = Constants.Messages.GetEstimationMessage(fullInfo, todoItem.DueDate);

        ChatResponse = await _chatBotService.GetResponse(fullInfo, ChatGPTService.DefaultModel);
        BackgroundTask = false;
    }

    [RelayCommand]
    private async Task GenerateTasks(string topic)
    {
        BackgroundTask = true;
        if (string.IsNullOrEmpty(topic))
        {             
            return;
        }
        string fullInfo = Constants.Messages.GetTaskListMessage(topic, 5);
        string response = await _chatBotService.GetResponse(fullInfo, ChatGPTService.DefaultModel);

        var tasks = response.ExtractTasks();
        foreach (var task in tasks)
        {
            TodoItem todoItem = new ()
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            };
            _allTodoItems.Add(new TodoItemViewModel(todoItem));
            TodoItems.Add(new TodoItemViewModel(todoItem));
        }
        BackgroundTask = false;
    }

    [RelayCommand]
    private async Task OpenAddItemPopup()
    {
        AddTodoItemViewModel addTodoItemViewModel = new();

        Window addItemPopup = new()
        {
            Content = new AddTodoItemView { DataContext = addTodoItemViewModel },
            Width = 600,
            Height = 350,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            //Icon = new WindowIcon("/Assets/avalonia-logo.ico")
        };

        addTodoItemViewModel.ClosePopup += () => addItemPopup.Close();

        Window? mainWindow = Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.MainWindow : null;
        await addItemPopup.ShowDialog(mainWindow);

        if (addTodoItemViewModel.IsValid)
        {
            var task = new TodoItemViewModel(addTodoItemViewModel.CreatedItem!);
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
}