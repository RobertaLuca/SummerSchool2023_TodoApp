using LLama;
using System.Threading.Tasks;
using System.IO;
using LLama.Common;
using LLama.Native;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using TodoApp.Services;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //var modelPath = @"D:\2023\Siemens\SS2023\v2\SummerSchool2023_TodoApp\TodoApp\TodoApp\LLamaModels\wizardLM-7B.ggmlv3.q5_0.bin";
            //var outputPath = @"D:\2023\Siemens\SS2023\v2\SummerSchool2023_TodoApp\TodoApp\TodoApp\LLamaModels\Q\quantized_model_llama-2-13b.q4_1.bin";
            //if (LLamaQuantizer.Quantize(modelPath, outputPath, LLamaFtype.LLAMA_FTYPE_MOSTLY_Q5_0))
            //{
            //    Console.WriteLine("Quantization succeed!");
            //}
            //else
            //{
            //    Console.WriteLine("Quantization failed!");
            //}


            //Console.Write("Please input your model path: ");
            //string modelPath = Console.ReadLine();
            ////modelPath = @"D:\2023\Siemens\SS2023\v2\SummerSchool2023_TodoApp\TodoApp\TodoApp\LLamaModels\wizardLM-7b.ggmlv3.q4_1.bin";
            ////modelPath = @"D:\2023\Siemens\SS2023\v2\SummerSchool2023_TodoApp\TodoApp\TodoApp\LLamaModels\llama-2-13b-chat.ggmlv3.q4_1.bin";
            ////modelPath = @"D:\2023\Siemens\SS2023\v2\SummerSchool2023_TodoApp\TodoApp\TodoApp\LLamaModels\wizardLM-7B.ggmlv3.q5_0.bin";
            //modelPath = @"D:\2023\Siemens\SS2023\v2\SummerSchool2023_TodoApp\TodoApp\TodoApp\LLamaModels\wizardLM-7B.ggmlv3.q5_0.bin";
            //InstructExecutor ex = new(new LLamaModel(new ModelParams(modelPath)));

            //Console.ForegroundColor = ConsoleColor.Green;
            //string prompt = Console.ReadLine();

            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine("The executor has been enabled. In this example, the LLM will follow your instructions. For example, you can input \"Write a story about a fox who want to " +
            //    "make friend with human, no less than 200 words.\"");
            //Console.ForegroundColor = ConsoleColor.White;

            //var inferenceParams = new InferenceParams() { Temperature = 0.2f, MaxTokens = 512 };

            //while (true)
            //{
            //    foreach (var text in ex.Infer(prompt, inferenceParams))
            //    {
            //        Console.Write(text);
            //    }
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    prompt = Console.ReadLine();
            //    Console.ForegroundColor = ConsoleColor.White;
            //}

            //Console.Write("Please input your model path: ");
            //string modelPath = Console.ReadLine();

            //StatelessExecutor ex = new(new LLamaModel(new ModelParams(modelPath)));

            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine("The executor has been enabled. In this example, the inference is an one-time job. That says, the previous input and response has " +
            //    "no impact on the current response. Now you can ask it questions. Note that in this example, no prompt was set for LLM and the maximum response tokens is 50. " +
            //    "It may not perform well because of lack of prompt. This is also an example that could indicate the improtance of prompt in LLM. To improve it, you can add " +
            //    "a prompt for it yourself!");
            //Console.ForegroundColor = ConsoleColor.White;

            //var inferenceParams = new InferenceParams() { Temperature = 0.4f, AntiPrompts = new List<string> { "Question:", "###END_GENERATION###", "Question: ", ".\n" }, MaxTokens = 256, TopP=0.95f };

            //while (true)
            //{
            //    Console.Write("\nQuestion: ");
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    string prompt = Console.ReadLine();
            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.Write("Answer: ");
            //    prompt = $"Question: {prompt.Trim()}";
            //    foreach (var text in ex.Infer(prompt, inferenceParams))
            //    {
            //        Console.Write(text);
            //    }
            //}



            //Console.Write("Please input your model path: ");
            //string modelPath = Console.ReadLine();

            //var ex = new StatelessExecutor(new LLamaModel(new ModelParams(modelPath)));

            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine("The executor has been enabled. In this example, the inference is an one-time job. That says, the previous input and response has " +
            //    "no impact on the current response. Now you can ask it questions. Note that in this example, no prompt was set for LLM and the maximum response tokens is 50. " +
            //    "It may not perform well because of lack of prompt. This is also an example that could indicate the improtance of prompt in LLM. To improve it, you can add " +
            //    "a prompt for it yourself!");
            //Console.ForegroundColor = ConsoleColor.White;

            //var inferenceParams = new InferenceParams() { Temperature = 0.6f, MaxTokens = 300, AntiPrompts = new List<string> { "###END_GENERATION###" } };

            //while (true)
            //{
            //    Console.Write("\nQuestion: ");
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    string prompt = Console.ReadLine();
            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.Write("Answer: ");
            //    prompt = $"Question: {prompt.Trim()} ###END_GENERATION###";
            //    foreach (var text in ex.Infer(prompt, inferenceParams))
            //    {
            //        Console.Write(text);
            //    }
            //}

            await ChatGPT();
        }

        private static async Task ChatGPT()
        {
            //string apiKey = "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";
            //IChatBotService chatBotService = new ChatGPTService(apiKey);

            //Console.WriteLine("Welcome to the ChatGPT chatbot! Type 'exit' to quit.");

            //// Enter a loop to take user input and display chatbot responses
            //while (true)
            //{
            //    // Prompt the user for input
            //    Console.ForegroundColor = ConsoleColor.Green; // Set text color to green
            //    Console.Write("You: ");
            //    Console.ResetColor(); // Reset text color to default
            //    string input = Console.ReadLine() ?? string.Empty;

            //    // Exit the loop if the user types "exit"
            //    if (input.ToLower() == "exit")
            //        break;

            //    if (input.Trim() == string.Empty)
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine("Please input something!");
            //        Console.ResetColor();
            //        continue;
            //    }

            //    // Send the user's input to the ChatGPT API and receive a response
            //    string response = await chatBotService.GetResponse(input, "gpt-3.5-turbo", Constants.Messages.GetTaskListMessage);

            //    // Display the chatbot's response
            //    Console.ForegroundColor = ConsoleColor.Blue; // Set text color to blue
            //    Console.Write("Chatbot: ");
            //    Console.ResetColor(); // Reset text color to default
            //    Console.WriteLine(response);
            //}
        }
    }
}