using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.ViewModels;

public sealed partial class ChatViewModel : ViewModelBase
{
	private readonly IChatBotService _chatBotService;

	[ObservableProperty]
	private string _chatResponse = string.Empty;

	public ChatViewModel(CurrentTodoService currentTodoService, IChatBotService chatBotService)
	{
		TodoItem = currentTodoService.CurrentTodo;
		_chatBotService = chatBotService;
	}

	public TodoItem? TodoItem { get; }

	[RelayCommand]
	private async Task AskForHelp()
	{
		if (TodoItem is not null)
		{
			ChatResponse = await _chatBotService.AskAdvice(TodoItem.Title, TodoItem.Description);
		}
	}
}
