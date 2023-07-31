using LLama;
using System.Threading.Tasks;
using System.IO;
using LLama.Common;
using System;

namespace TodoApp.Services;

public class LLamaGPTService : IChatBotService
{

    public async Task<string> GetResponse(
        string message,
        string model = "llama-2-13b-chat.ggmlv3.q4_1.bin",
        Func<string, string>? postProcess = null)
    {
        string workingDirectory = Environment.CurrentDirectory;
        var LLamaModel = new LLamaModel(new ModelParams(
            modelPath: Path.Combine("..", "..", "..", "LLamaModels", "llama-2-13b-chat.ggmlv3.q4_1.bin"),
            contextSize: 256));

        InstructExecutor instructExecutor = new(LLamaModel);

        var inferenceParams = new InferenceParams() { Temperature = 0.8f, MaxTokens = 300 };

        string response = "";
        await foreach (var text in instructExecutor.InferAsync(message, inferenceParams))
        {
            response += text;
        }

        return response;
    }
}
