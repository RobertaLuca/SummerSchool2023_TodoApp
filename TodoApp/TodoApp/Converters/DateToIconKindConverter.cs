using Avalonia.Data.Converters;
using Material.Icons;
using System.Globalization;

namespace TodoApp.Converters;

public class DateToIconKindConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateOnly dateOnly && dateOnly >= DateOnly.FromDateTime(DateTime.Now))
        {
            return MaterialIconKind.CalendarEditOutline;
        }

        return MaterialIconKind.CalendarRemoveOutline;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
