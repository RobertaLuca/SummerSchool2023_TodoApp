using Avalonia;
namespace TodoApp.ViewModels
{
    using Avalonia.Controls;
    using Avalonia.Controls.ApplicationLifetimes;
    using System.Collections.ObjectModel;
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
        }

        public AddTodoItemViewModel AddTodoItemViewModel
        {
            get => _addTodoItemViewModel;
            set => SetProperty(ref _addTodoItemViewModel, value);
        }

        public ObservableCollection<TodoItem> TodoItems { get; } = new();

        public DelegateCommand OpenPopupCommand { get; }

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
    }
}