using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TodoApp.Helper;

public class ObservableObject : INotifyPropertyChanged
{
    protected virtual bool SetProperty<T>(
        ref T backingStore, T value,
        Action? onChanged = null,
        Func<T, T, bool>? validateValue = null,
        [CallerMemberName] string propertyName = "")
    {
        //if value didn't change
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        //if value changed but didn't validate
        if (validateValue != null && !validateValue(backingStore, value))
            return false;

        backingStore = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);
        return true;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
