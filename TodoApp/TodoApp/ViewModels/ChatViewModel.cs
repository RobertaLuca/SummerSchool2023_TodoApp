using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.ViewModels;

public sealed partial class ChatViewModel : ViewModelBase
{
	private readonly NavigationService _navigationService;

	[ObservableProperty]
	private string _chatResponse = string.Empty;

	public ChatViewModel(CurrentTodoService currentTodoService, IChatBotService chatBotService, NavigationService navigationService)
	{
		TodoItem = currentTodoService.CurrentTodo;
        ChatBotService = chatBotService;

		_navigationService = navigationService;
	}

	public TodoItem? TodoItem { get; }
    public IChatBotService ChatBotService { get; }

    [RelayCommand]
	private async Task AskForHelp()
	{
		if (TodoItem is not null)
		{
			ChatResponse = await ChatBotService.AskAdvice(TodoItem.Title, TodoItem.Description);
		}
	}

	[RelayCommand]
	private void GoBack()
	{
		_navigationService.Navigate<TodoItemsViewModel>();
	}
}
