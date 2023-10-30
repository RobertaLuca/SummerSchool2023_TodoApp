using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Material.Icons;

namespace TodoApp.Views;

public class IconText : TemplatedControl
{
    public static readonly StyledProperty<string> TextProperty = AvaloniaProperty.Register<IconText, string>(nameof(Text));
    public static readonly StyledProperty<MaterialIconKind> IconProperty = AvaloniaProperty.Register<IconText, MaterialIconKind>(nameof(Icon));
    public static readonly StyledProperty<HorizontalAlignment> HorizontalContentAlignmentProperty = 
        AvaloniaProperty.Register<IconText, HorizontalAlignment>(nameof(HorizontalContentAlignment), HorizontalAlignment.Center);

    public HorizontalAlignment HorizontalContentAlignment
    {
        get => GetValue(HorizontalContentAlignmentProperty);
        set => SetValue(HorizontalContentAlignmentProperty, value);
    }

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public MaterialIconKind Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
}