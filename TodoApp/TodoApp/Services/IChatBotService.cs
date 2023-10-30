namespace TodoApp.Services;

public interface IChatBotService
{
	public Task<string> AskAdvice(string todoTitle, string todoMessage);

	public string SystemPrompt { get; set; }
}
