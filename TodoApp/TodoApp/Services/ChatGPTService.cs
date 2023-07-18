using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReactiveUI;
using RestSharp;

namespace TodoApp.Services;

public class ChatGPTService : IChatBotService
{
    private readonly string _apiKey;
    private readonly RestClient _client;

    public ChatGPTService(string apiKey)
    {
        _apiKey = apiKey;
        
        // Initialize the RestClient with the ChatGPT API endpoint
        _client = new RestClient("https://api.openai.com/v1/chat/completions");
    }
    
    public async Task<string> GetResponse(string message, string model)
    {
        string errorMessage;
        if (!Validate(message, out errorMessage))
        {
            return errorMessage;
        }

        try
        {
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            // Set the Authorization header with the API key

            request.AddHeader("Authorization", $"Bearer {_apiKey}");
            var requestBody = new
            {
                model = model!,
                messages = $"Tell me how much that these products weight " +
                    $"[NO OTHER WORDS, NOT A SINGLE WORD PLEASE JUST THE NUMBER AND THAT'S ALL," +
                    $" NO GREETINGS OR A MESSAGE TO TELL THAT YOU UNDERSTOOD THE JOB, JUST THE NUMBER]:" +
                    $" {message}",
                temperature = 0.5,
                max_tokens = 256,
                top_p = 1,
                frequency_penalty = 0,
                presence_penalty = 0
            };

            request.AddJsonBody(JsonConvert.SerializeObject(requestBody));

            var response = await _client.ExecuteAsync(request);

            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content ?? string.Empty);
            return jsonResponse?.choices[0]?.text?.ToString()?.Trim() ?? string.Empty;
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during the API request
            Console.WriteLine($"Error: {ex.Message}");
            return "Sorry, there was an error processing your request. Please try again later.";
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
}
