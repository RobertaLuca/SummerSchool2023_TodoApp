using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using RestSharp;

namespace TodoApp.Services;

public class ChatGPTService : IChatBotService, IDisposable
{
    private readonly string _apiKey;
    private readonly RestClient _client;

    public static string DefaultModel => "gpt-3.5-turbo";

    public ChatGPTService(string apiKey)
    {
        _apiKey = apiKey;
        // Initialize the RestClient with the ChatGPT API endpoint
        _client = new RestClient("https://api.openai.com/v1/chat/completions");
    }
    
    public async Task<string> GetResponse(string message, string model, Func<string, string>? postProcess = null)
    {
        string errorMessage;
        if (!Validate(message, out errorMessage))
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
            request.AddJsonBody(JsonConvert.SerializeObject(requestBody));

            // Execute the request and receive the response
            var response = await _client.ExecuteAsync(request);

            JObject json = JObject.Parse(response.Content ?? string.Empty);
            var response_message = json?["choices"]?[0]?["message"]?["content"];

            // Extract and return the chatbot's response text
            return response_message?.ToString()?.Trim() ?? String.Empty;
        }
        catch (Exception ex)
        {
            return $"Sorry, there was an error processing your request: {ex}. Please try again later.";
        }
    }
    
    private bool Validate(string message, out string error)
    {
        bool isValid = !string.IsNullOrWhiteSpace(message);
        error = "";
        
        if (!isValid)
        {
            error = "Sorry, I didn't receive any input. Please try again!";
        }

        return isValid;
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
}
