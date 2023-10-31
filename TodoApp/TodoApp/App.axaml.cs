using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Core;
using TodoApp.Services;
using TodoApp.ViewModels;
using TodoApp.Views;

namespace TodoApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        ServiceCollection serviceCollection = new();
        serviceCollection.AddSingleton(serviceCollection)
            .AddSingleton<NavigationService>()
            .AddSingleton<Configs>()
            .AddSingleton<CurrentTodoService>()
            .AddSingleton<IChatBotService, ChatGPTService>()
            .AddSingleton<TodoItemsViewModel>()
            .AddSingleton<TodoItemsView>()
            .AddSingleton<MainWindowViewModel>()
            .AddScope<ChatViewModel>()
            .AddSingleton<ChatWindow>()
            .AddSingleton<OptionsViewModel>()
            .AddSingleton<OptionsPage>();

        NavigationService navigationService = serviceCollection.GetService<NavigationService>()
			.RegisterPage<TodoItemsView, TodoItemsViewModel>("Todo Items")
			.RegisterPage<ChatWindow, ChatViewModel>("Chat")
			.RegisterPage<OptionsPage, OptionsViewModel>("Options");

        navigationService.Navigate<TodoItemsViewModel>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = serviceCollection.GetService<MainWindowViewModel>()
            };
        }
      
        base.OnFrameworkInitializationCompleted();
    }
}
