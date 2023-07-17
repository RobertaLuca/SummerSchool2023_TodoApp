using System;
using TodoApp.Helper;
using TodoApp.Models;

namespace TodoApp.ViewModels
{
    public sealed partial class AddTodoItemViewModel : ViewModelBase
    {
        private string _title;
        private string _description;
        private DateTime _dueDate;

        public AddTodoItemViewModel()
        {
            SaveItemCommand = new DelegateCommand(SaveItem);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public DateTime DueDate
        {
            get => _dueDate;
            set => SetProperty(ref _dueDate, value);
        }

        public DelegateCommand SaveItemCommand { get; }

        private void SaveItem()
        {
            //TodoItem todoItem = new TodoItem(Title, Description, DueDate);

        }
    }
}
