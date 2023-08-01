using RestSharp;
using System.Text.Json;

namespace TodoApp.Services;

public class ChatGPTService : IChatBotService
{
    private readonly string _apiKey;

    public static string DefaultModel => "gpt-3.5-turbo";

    public ChatGPTService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<string> GetResponse(string message, string model, Func<string, string>? postProcess = null)
    {
        // Initialize the RestClient with the ChatGPT API endpoint
        using RestClient client = new RestClient("https://api.openai.com/v1/chat/completions");
        if (!Validate(message, out string errorMessage))
        {
            return errorMessage;
        }

        message = message.Trim();
        message = postProcess?.Invoke(message) ?? message;

        try
        {
            // Create a new POST request
            var request = new RestRequest("", Method.Post);
            // Set the Content-Type header
            request.AddHeader("Content-Type", "application/json");
            // Set the Authorization header with the API key
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            // Create the request body with the message and other parameters
            var requestBody = new
            {
                model = model!,
                messages = new[]{
                        new
                        {
                            role = "user",
                            content = message
                        }
                    },
                max_tokens = 512,
                n = 1,
                stop = (string?)null,
                temperature = 0.7,
            };

            // Add the JSON body to the request
            request.AddJsonBody(JsonSerializer.Serialize(requestBody));

            // Execute the request and receive the response
            var response = await client.ExecuteAsync(request);

            var json = JsonDocument.Parse(response.Content ?? string.Empty);
            var response_message = json.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? string.Empty;

            // Extract and return the chatbot's response text
            return response_message.Trim();
        }
        catch (Exception ex)
        {
            return $"Sorry, there was an error processing your request: {ex}. Please try again later.";
        }
    }

    private static bool Validate(string message, out string error)
    {
        bool isValid = !string.IsNullOrWhiteSpace(message);
        error = isValid ? "" : "Sorry, I didn't receive any input. Please try again!";
        return isValid;
    }

    //public void Dispose()
    //{
    //    _client?.Dispose();
    //    GC.SuppressFinalize(this);
    //}
}
