using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.ViewModels;

public sealed partial class ChatViewModel : ViewModelBase
{

	[ObservableProperty]
	private string _chatResponse = string.Empty;

	public ChatViewModel(CurrentTodoService currentTodoService, IChatBotService chatBotService)
	{
		TodoItem = currentTodoService.CurrentTodo;
        ChatBotService = chatBotService;
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
}
