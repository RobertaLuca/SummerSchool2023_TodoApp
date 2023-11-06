using Avalonia;
using Avalonia.Controls.Primitives;
using Material.Icons;

namespace TodoApp.Views;

// TODO: this is just a skeleton
// the purpose of the control would be to handle a toggle button with custom on and off icons
// this will replace the styles used to handle this case, in order to have more generic functionality that can handle different kind of icons
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