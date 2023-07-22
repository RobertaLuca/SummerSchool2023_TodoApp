using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Helpers;

public class AsyncRelayCommand<T> : AsyncCommandBase<T> where T : class
{
    private readonly Func<T, Task> _callback;
    
    public AsyncRelayCommand(Func<T, Task> callback, Action<Exception> onException) : base(onException)
    {
        _callback = callback;
    }

    protected override async Task ExecuteAsync(T parameter)
    {
        await _callback(parameter);
    }
}
