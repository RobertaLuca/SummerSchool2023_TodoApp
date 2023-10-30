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
            .AddSingleton<PageService>()
            .AddSingleton<Configs>()
            .AddSingleton<CurrentTodoService>()
            .AddSingleton<IChatBotService, ChatGPTService>()
            .AddSingleton<TodoItemsViewModel>()
            .AddSingleton<TodoItemsView>()
            .AddSingleton<MainWindowViewModel>()
            .AddScope<TodoChatWindowViewModel>()
            .AddSingleton<ChatWindow>();

        PageService pageService = serviceCollection.GetService<PageService>()
            .RegisterPage<TodoItemsView, TodoItemsViewModel>("Todo Items")
            .RegisterPage<ChatWindow, TodoChatWindowViewModel>("Chat");

        NavigationService navigationService = serviceCollection.GetService<NavigationService>();
        navigationService.CurrentPageType = pageService.Pages[typeof(TodoItemsView)];

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
