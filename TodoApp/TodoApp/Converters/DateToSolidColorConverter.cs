using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System.Globalization;

namespace TodoApp.Converters;

public sealed class DateToSolidColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateOnly dateOnly)
        {
            SolidColorBrush errorSolidColor = new(Colors.Red);
            SolidColorBrush defaultSolidColor = new(Colors.Gray);
            SolidColorBrush warningSolidColor = new(Colors.Yellow);

            if (Application.Current?.TryFindResource("SystemErrorTextColor", out var errorColor) is true && errorColor is Color ec)
            {
                errorSolidColor = new(ec);
            }

            if (Application.Current?.TryFindResource("SystemBaseHighColor", out var defaultColor) is true && defaultColor is Color dc)
            {
                defaultSolidColor = new(dc);
            }

            if (Application.Current?.TryFindResource("Warning", out var warningColor) is true && warningColor is ISolidColorBrush wc)
            {
                warningSolidColor = new(wc.Color);
            }

            return dateOnly.CompareTo(DateOnly.FromDateTime(DateTime.Now)) switch
            {
                1 => defaultSolidColor,
                0 => warningSolidColor,
                _ => errorSolidColor,
            };
        }

        return Brushes.Transparent;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null!;
    }
}
