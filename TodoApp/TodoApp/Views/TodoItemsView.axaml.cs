using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace TodoApp.Views;

public partial class TodoItemsView : UserControl
{
    public TodoItemsView()
    {
        InitializeComponent();
    }

    private void SearchInput_KeyDown(object sender, KeyEventArgs eventArgs)
    {
        if (eventArgs.Key == Key.Enter)
        {
            if (sender is TextBox textBox)
            {
                var searchButton = this.FindControl<Button>("GenerateButton");
                searchButton.Command.Execute(textBox.Text);
            }
        }
    }
}