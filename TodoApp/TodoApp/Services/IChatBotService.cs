namespace TodoApp.Services;

public interface IChatBotService
{
    public Task<string> GetResponse(string message, string model, Func<string, string>? postProcess = null);
}
