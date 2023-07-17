using System;
using TodoApp.Helper;
using TodoApp.Models;

namespace TodoApp.ViewModels
{
    public sealed partial class AddTodoItemViewModel : ViewModelBase
    {
        private string _title;
        private string _description;
        private DateTimeOffset? _dueDate;

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

        public DateTimeOffset? DueDate
        {
            get => _dueDate;
            set => SetProperty(ref _dueDate, value);
        }

        public TodoItem CreatedItem
        {
            get
            {
                if (Title == null || Description == null || DueDate == null)
                {
                    return null;
                }

                return new TodoItem(Title, Description, DueDate.Value.DateTime);
            }
        }

        public DelegateCommand SaveItemCommand { get; }

        private void SaveItem()
        {

        }
    }
}
