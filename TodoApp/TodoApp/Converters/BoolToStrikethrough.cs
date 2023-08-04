using Avalonia.Data.Converters;
using Avalonia.Media;
using System.Globalization;

namespace TodoApp.Converters
{
    public class BoolToStrikethrough : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool myBoolean)
            {
                if (myBoolean == true)
                {
                    return TextDecorations.Strikethrough;
                }
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
