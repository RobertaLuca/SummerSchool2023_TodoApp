using Avalonia;
using Avalonia.Controls.Primitives;
using Material.Icons;

namespace TodoApp.Views;

public class IconToggleButton : ToggleButton
{
    public static readonly StyledProperty<MaterialIconKind> OnIconProperty = AvaloniaProperty.Register<IconToggleButton, MaterialIconKind>(nameof(OnIcon));
    public static readonly StyledProperty<MaterialIconKind> OffIconProperty = AvaloniaProperty.Register<IconToggleButton, MaterialIconKind>(nameof(OffIcon));

    public MaterialIconKind OnIcon
    {
        get => GetValue(OnIconProperty);
        set => SetValue(OnIconProperty, value);
    }

    public MaterialIconKind OffIcon
    {
        get => GetValue(OffIconProperty);
        set => SetValue(OffIconProperty, value);
    }
}