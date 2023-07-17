namespace TodoApp.ViewModels
{
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Controls.ApplicationLifetimes;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using TodoApp.Helper;
    using TodoApp.Models;
    using TodoApp.Views;

    public sealed partial class TodoItemsViewModel : ViewModelBase
    {
        private AddTodoItemViewModel _addTodoItemViewModel;

        public TodoItemsViewModel()
        {
            _addTodoItemViewModel = new AddTodoItemViewModel();

            OpenPopupCommand = new DelegateCommand(OpenAddItemPopup);
            GetNotDueItemsCommand = new DelegateCommand(GetNotDueItems);
            GetAllItemsCommand = new DelegateCommand(GetAllItems);
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

        public DelegateCommand OpenPopupCommand { get; }

        public DelegateCommand GetNotDueItemsCommand { get; }

        public DelegateCommand GetAllItemsCommand { get; }

        private void RemoveTodoItem(TodoItem item)
        {
            TodoItems.Remove(item);
        }

        private async void OpenAddItemPopup()
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