using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace TodoApp.Converters;

internal class DateToSolidColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateOnly dateOnly)
        {
            object? errorColor = null;
            object? defaultColor = null;
            object? warningColor = null;
            Application.Current?.TryFindResource("SystemErrorTextColor", out errorColor);
            Application.Current?.TryFindResource("SystemBaseHighColor", out defaultColor);
            Application.Current?.TryFindResource("Warning", out warningColor);

            SolidColorBrush errorSolidColor = new SolidColorBrush(Colors.Red);
            SolidColorBrush defaultSolidColor = new SolidColorBrush(Colors.Gray);
            SolidColorBrush warningSolidColor = new SolidColorBrush(Colors.Yellow);

            if (errorColor is not null and Color ec)
            {
                errorSolidColor = new SolidColorBrush(ec);
            }

            if (defaultColor is not null and Color dc)
            {
                defaultSolidColor = new SolidColorBrush(dc);
            }

            if (warningColor is not null and ISolidColorBrush wc)
            {
                warningSolidColor = new SolidColorBrush(wc.Color);
            }

            switch (dateOnly.CompareTo(DateOnly.FromDateTime(DateTime.Now)))
            {
                case 1:
                    return defaultSolidColor;
                case 0:
                    return warningSolidColor;
                default:
                    return errorSolidColor;
            }
        }

        return Brushes.Transparent;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
