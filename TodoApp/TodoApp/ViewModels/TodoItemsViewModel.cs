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
    using System.Threading.Tasks;
    using System.Windows.Input;
    using TodoApp.Helper;
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
            //_chatBotService = chatBotService;

            OpenPopupCommand = new Helpers.AsyncRelayCommand<string>(OpenAddItemPopup, (ex) => { });
            GetNotDueItemsCommand = new DelegateCommand(GetNotDueItems);
            GetAllItemsCommand = new DelegateCommand(GetAllItems);
            DeleteCommand = new Helpers.AsyncRelayCommand<TodoItem>(DeleteItem, (ex) => { });
            SetDoneCommand = new Helpers.RelayCommand<TodoItem>(MarkAsDone);

            CalculateMassCommand = new Helpers.AsyncRelayCommand<TodoItem>(CalculateMass, (ex) => { });
        }

        private void MarkAsDone(TodoItem item)
        {
            var index = TodoItems.IndexOf(item);

            TodoItems.RemoveAt(index);
            item.IsDone = true;
            TodoItems.Insert(index, item);
        }

        private async Task DeleteItem(TodoItem? item)
        {
            if (item is not null)
            {
                TodoItems.Remove(item);
            }
        }

        public AddTodoItemViewModel AddTodoItemViewModel
        {
            get => _addTodoItemViewModel;
            set => SetProperty(ref _addTodoItemViewModel, value);
        }

        // initial implementation
        //public ObservableCollection<TodoItem> TodoItems { get; } = new();

        // for filtering
        private IList<TodoItem> _allTodoItems = new List<TodoItem>();

        private ObservableCollection<TodoItem> _todoItems = new ObservableCollection<TodoItem>();
        public ObservableCollection<TodoItem> TodoItems
        {
            get => _todoItems;
            set => SetProperty(ref _todoItems, value);
        }

        public Helpers.AsyncRelayCommand<string> OpenPopupCommand { get; }
        public Helpers.RelayCommand<TodoItem> SetDoneCommand { get; }
        public Helpers.AsyncRelayCommand<TodoItem> DeleteCommand { get; }

        public DelegateCommand GetNotDueItemsCommand { get; }

        public DelegateCommand GetAllItemsCommand { get; }
        public Helpers.AsyncRelayCommand<TodoItem> CalculateMassCommand { get; }

        private async Task CalculateMass(TodoItem todoItem)
        {
            // await _chatBotService.GetResponse(todoItem.Description);
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
                TodoItems.Add(_addTodoItemViewModel.CreatedItem);
            }
        }

        private void GetNotDueItems()
        {
            _allTodoItems = TodoItems;
            var notDueItems = TodoItems.OrderBy(item => item.DueDate).Where(item => item.DueDate >= DateTime.Now.Date).ToList();
            TodoItems = new ObservableCollection<TodoItem>(notDueItems);
        }

        private void GetAllItems()
        {
            TodoItems = new ObservableCollection<TodoItem>(_allTodoItems);
        }
    }
}