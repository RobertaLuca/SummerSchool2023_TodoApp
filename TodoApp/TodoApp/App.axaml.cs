﻿using Avalonia;
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
            .AddScope<ChatViewModel>()
            .AddSingleton<ChatWindow>()
            .AddSingleton<OptionsViewModel>()
            .AddSingleton<OptionsPage>();

        PageService pageService = serviceCollection.GetService<PageService>()
            .RegisterPage<TodoItemsView, TodoItemsViewModel>("Todo Items")
            .RegisterPage<ChatWindow, ChatViewModel>("Chat")
            .RegisterPage<OptionsPage, OptionsViewModel>("Options");

        NavigationService navigationService = serviceCollection.GetService<NavigationService>();
        navigationService.CurrentPageData = pageService.Pages[typeof(TodoItemsViewModel)];

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
