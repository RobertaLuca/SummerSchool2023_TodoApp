using TodoApp.Services;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            const string API_KEY = "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";
            IChatBotService chatBotService = new ChatGPTService(API_KEY);

            Console.WriteLine("Welcome to the ChatGPT chatbot! Type 'exit' to quit.");

            // Enter a loop to take user input and display chatbot responses
            while (true)
            {
                // Prompt the user for input
                Console.ForegroundColor = ConsoleColor.Green; // Set text color to green
                Console.Write("You: ");
                Console.ResetColor(); // Reset text color to default
                string input = Console.ReadLine() ?? string.Empty;

                // Exit the loop if the user types "exit"
                if (input.ToLower() == "exit")
                    break;

                // Send the user's input to the ChatGPT API and receive a response
                string response = await chatBotService.GetResponse(input);

                // Display the chatbot's response
                Console.ForegroundColor = ConsoleColor.Blue; // Set text color to blue
                Console.Write("Chatbot: ");
                Console.ResetColor(); // Reset text color to default
                Console.WriteLine(response);
            }
        }
    }
}