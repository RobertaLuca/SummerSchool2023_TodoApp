using Avalonia.Data.Converters;
using System;
using System.Globalization;
using Material.Icons;

namespace TodoApp.Converters;

public class DateToIconKindConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateOnly dateOnly)
        {
            switch (dateOnly.CompareTo(DateOnly.FromDateTime(DateTime.Now)))
            {
                case 1:
                    return MaterialIconKind.CalendarEditOutline;
                case 0:
                    return MaterialIconKind.CalendarEditOutline;
                default:
                    return MaterialIconKind.CalendarRemoveOutline;
            }
        }

        return MaterialIconKind.CalendarRemoveOutline;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
