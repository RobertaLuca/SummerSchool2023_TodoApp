using Azure.AI.OpenAI;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TodoApp.Services;

public partial class ChatGPTService : ObservableObject, IChatBotService
{
	private readonly Configs _configs;

	[ObservableProperty]
	private string _systemPrompt = "You are a virtual assistant that help students with different kind of questions, where most of them are programming related";

    public ChatGPTService(Configs configs)
    {
		_configs = configs;
	}

    public async Task<string> AskAdvice(string todoTitle, string todoMessage)
	{
		string aoaiEndpoint = _configs.GetSetting("api-url") ?? throw new ArgumentNullException();
		string apiKey = _configs.GetSetting("api-key") ?? throw new ArgumentNullException();
		string model = "gpt-35-turbo";

		var endpoint = new Uri(aoaiEndpoint);
		var credentials = new Azure.AzureKeyCredential(key: apiKey);
		var aiClient = new OpenAIClient(endpoint, credentials);

		string userPrompt = $"I want to do: {todoTitle}. Here are a few more details {todoMessage}. How can I do this?";

		ChatCompletionsOptions completionOptions = new()
		{
			MaxTokens = 500,
			Temperature = 0f,
			FrequencyPenalty = 0.0f,
			PresencePenalty = 0.0f,
		};

		completionOptions.Messages.Add(new ChatMessage(ChatRole.System, SystemPrompt));
		completionOptions.Messages.Add(new ChatMessage(ChatRole.User, userPrompt));

		ChatCompletions response = await aiClient.GetChatCompletionsAsync(model, completionOptions);

		return response.Choices[0].Message.Content;
	}
}

