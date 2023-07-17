namespace TodoApp.ViewModels
{
    using System;
    using TodoApp.Helper;
    using TodoApp.Models;

    public sealed partial class AddTodoItemViewModel : ViewModelBase
    {
        private string _title = string.Empty;
        private string _description = string.Empty;
        private DateTimeOffset? _dueDate;
        private bool _isValid = false;
        private TodoItem _createdItem;

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
        
        public bool IsValid 
        {
            get => _isValid;
            set 
            {
                _isValid = value;
            } 
        }

        public TodoItem CreatedItem
        {
            get => _createdItem;
            set
            {
                _createdItem = value;
            }
        }

        public DelegateCommand SaveItemCommand { get; }

        public Action ClosePopup { get; set; }

        private void SaveItem()
        {
            if (Title == null || Description == null || DueDate == null ||
                    Title == string.Empty || Description == string.Empty)
            {
                _isValid = false;
            }
            else
            {
                _isValid = true;
                CreatedItem = new TodoItem(Title, Description, DueDate.Value.DateTime);
            }

            ClosePopup?.Invoke();

            ResetFields();
        }

        private void ResetFields()
        {
            _title = string.Empty;
            _description = string.Empty;
            _dueDate = null;
            _isValid = false;
        }
    }
}
