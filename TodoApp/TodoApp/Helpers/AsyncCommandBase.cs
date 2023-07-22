﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TodoApp.Helpers;

public abstract class AsyncCommandBase<T> : ICommand where T : class
{
    private readonly Action<Exception>? _onException;

    private bool _isExecuting;
    public bool IsExecuting
    {
        get
        {
            return _isExecuting;
        }
        set
        {
            _isExecuting = value;
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }

    public event EventHandler? CanExecuteChanged;

    public AsyncCommandBase(Action<Exception> onException)
    {
        _onException = onException;
    }

    public bool CanExecute(object parameter)
    {
        return !IsExecuting;
    }

    public async void Execute(object parameter)
    {
        IsExecuting = true;

        try
        {
            await ExecuteAsync((T)parameter);
        }
        catch (Exception ex)
        {
            _onException?.Invoke(ex);
        }

        IsExecuting = false;
    }

    protected abstract Task ExecuteAsync(T parameter);
}
