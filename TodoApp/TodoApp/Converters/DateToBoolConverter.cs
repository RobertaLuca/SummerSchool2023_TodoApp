using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace TodoApp.Converters;

public class DateToBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateOnly dateOnly)
        {
            return dateOnly < DateOnly.FromDateTime(DateTime.Now);
        }

        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
