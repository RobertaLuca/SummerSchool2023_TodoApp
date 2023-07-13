﻿namespace TodoApp.Models
{
    using System;

    internal interface ITodoItem
    {
        string Title { get; }
       
        string Description { get; }

        DateOnly DueDate { get; }
    }
}