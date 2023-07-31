using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TodoApp.Helpers;
using TodoApp.Services;
using TodoApp.Views;

namespace TodoApp.ViewModels
{
    public sealed partial class TodoItemsViewModel : ViewModelBase
    {
        private readonly IChatBotService _chatBotService;

        public TodoItemsViewModel()
        {
            _chatBotService = new ChatGPTService(Utils.ReadSetting("OPENAI_API_KEY"));
        }

        [RelayCommand]
        private void DeleteItem(TodoItemViewModel? item)
        {
            if (item is not null)
            {
                TodoItems.Remove(item);
            }
        }

        [ObservableProperty]
        private string? _chatResponse;

        // initial implementation
        //public ObservableCollection<TodoItem> TodoItems { get; } = new();

        // for filtering
        private ObservableCollection<TodoItemViewModel> _allTodoItems = new();

        [ObservableProperty]
        private ObservableCollection<TodoItemViewModel> _todoItems = new();

        [RelayCommand]
        private async Task CalculateMass(TodoItemViewModel todoItem)
        {
            if (todoItem.IsDone)
            {
                ChatResponse = "Congratulations on the completed task!";
                return;
            }

            var fullInfo = $"{todoItem.Title} ({todoItem.Description})";
            fullInfo = Constants.Messages.GetEstimationMessage(fullInfo, todoItem.DueDate);

            ChatResponse = await _chatBotService.GetResponse(fullInfo, ChatGPTService.DefaultModel);
        }

        [RelayCommand]
        private async Task OpenAddItemPopup(string lUnused)
        {
            AddTodoItemViewModel addTodoItemViewModel = new();

            Window addItemPopup = new()
            {
                Content = new AddTodoItemView { DataContext = addTodoItemViewModel },
                Width = 600,
                Height = 350,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };

            addTodoItemViewModel.ClosePopup += () => addItemPopup.Close();

            Window? mainWindow = Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.MainWindow : null;
            await addItemPopup.ShowDialog(mainWindow);

            if (addTodoItemViewModel.IsValid)
            {
                TodoItems.Add(new TodoItemViewModel(addTodoItemViewModel.CreatedItem!));
            }
        }

        [RelayCommand]
        public void GetNotDueItems()
        {
            _allTodoItems = TodoItems;
            TodoItems = new(TodoItems.OrderBy(x => x.DueDate).Where(x => x.DueDate >= DateOnly.FromDateTime(DateTime.Now)));
        }

        [RelayCommand]
        public void GetAllItems()
        {
            TodoItems = _allTodoItems;
        }
    }
}