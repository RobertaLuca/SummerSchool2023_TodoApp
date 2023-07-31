namespace TodoApp.ViewModels
{
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Controls.ApplicationLifetimes;
    using CommunityToolkit.Mvvm.Input;
    using DynamicData.Aggregation;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using TodoApp.Helper;
    using TodoApp.Helpers;
    using TodoApp.Models;
    using TodoApp.Services;
    using TodoApp.Views;

    public sealed partial class TodoItemsViewModel : ViewModelBase
    {
        private AddTodoItemViewModel _addTodoItemViewModel;
        // TO DO: Add dependecy injection
        private IChatBotService _chatBotService;
        public TodoItemsViewModel(/*IChatBotService chatBotService*/)
        {
            _addTodoItemViewModel = new AddTodoItemViewModel();
            _chatBotService = new ChatGPTService(Utils.ReadSetting("OPENAI_API_KEY"));

            OpenPopupCommand = new AsyncRelayCommand<string>(OpenAddItemPopup);
            //GetNotDueItemsCommand = new DelegateCommand(GetNotDueItems);
            //GetAllItemsCommand = new DelegateCommand(GetAllItems);
            DeleteCommand = new AsyncRelayCommand<TodoItemViewModel>(DeleteItem);

            CalculateMassCommand = new AsyncRelayCommand<TodoItemViewModel>(CalculateMass);
        }

        private async Task DeleteItem(TodoItemViewModel? item)
        {
            if (item is not null)
            {
                TodoItems.Remove(item);
            }
        }

        private string? _charResponse;
        public string? ChatResponse
        {
            get => _charResponse;
            set => SetProperty(ref _charResponse, value);
        }

        public AddTodoItemViewModel AddTodoItemViewModel
        {
            get => _addTodoItemViewModel;
            set => SetProperty(ref _addTodoItemViewModel, value);
        }

        // for filtering
        private IList<TodoItem> _allTodoItems = new List<TodoItem>();

        private ObservableCollection<TodoItemViewModel> _todoItems = new ObservableCollection<TodoItemViewModel>();
        public ObservableCollection<TodoItemViewModel> TodoItems
        {
            get => _todoItems;
            set => SetProperty(ref _todoItems, value);
        }

        public AsyncRelayCommand<string> OpenPopupCommand { get; }
        public AsyncRelayCommand<TodoItemViewModel> DeleteCommand { get; }

        public DelegateCommand GetNotDueItemsCommand { get; }

        public DelegateCommand GetAllItemsCommand { get; }
        public AsyncRelayCommand<TodoItemViewModel> CalculateMassCommand { get; }

        private async Task CalculateMass(TodoItemViewModel todoItem)
        {
            if (todoItem.IsDone)
            {
                ChatResponse = "Congratulations on the completed task!";
                return;
            }

            var fullInfo = $"{todoItem.Title} ({todoItem.Description})";
            fullInfo = Constants.Messages.getEstimationMessage(fullInfo, todoItem.DueDate);

            ChatResponse = await _chatBotService.GetResponse(fullInfo, ChatGPTService.DefaultModel);
        }

        private async Task OpenAddItemPopup(string lUnused)
        {
            var addItemPopup = new Window()
            {
                Content = new AddTodoItemView { DataContext = AddTodoItemViewModel },
                Width = 600,
                Height = 350,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };

            _addTodoItemViewModel.ClosePopup = () =>
            {
                addItemPopup.Close();
            };

            var mainWindow = Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.MainWindow : null;
            await addItemPopup.ShowDialog(mainWindow);

            if (_addTodoItemViewModel.IsValid)
            {
                TodoItems.Add(new TodoItemViewModel(_addTodoItemViewModel.CreatedItem));
            }
        }
    }
}