using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.ViewModels
{
    public class TodoItemViewModel : ViewModelBase
    {
        private string _title;
        private string _description;
        private DateOnly _dueDate;
        private bool _isDone;

        public TodoItemViewModel(string title, string description, DateOnly dueDate, bool isDone = false)
        {
            _title = title;
            _description = description;
            _dueDate = dueDate;
            _isDone = isDone;
        }

        public TodoItemViewModel(TodoItem item)
        {
            _title = item.Title;
            _description = item.Description;
            _dueDate = item.DueDate;
            _isDone = item.IsDone;
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

        public DateOnly DueDate
        {
            get => _dueDate;
            set => SetProperty(ref _dueDate, value);
        }

        public bool IsDone
        {
            get => _isDone;
            set => SetProperty(ref _isDone, value);
        }
    }
}
