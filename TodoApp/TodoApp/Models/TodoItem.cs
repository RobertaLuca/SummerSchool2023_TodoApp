namespace TodoApp.Models
{
    using System;

    public class TodoItem : ITodoItem
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsDone { get; set; }

        public TodoItem(string title, string description, DateTime dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
        }
    }
}
