using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;

namespace SK_HuggingFace_Experiment
{
    internal class Program
    {
        static async Task Main3(string[] args)
        {
            Console.WriteLine("Hello, World!");


#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.


            var kernel = Kernel.CreateBuilder()
                .AddHuggingFaceChatCompletion("gpt2",
                    endpoint: new Uri("https://api-inference.huggingface.co/models/openai-community/gpt2"),
                    apiKey: Environment.GetEnvironmentVariable("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"))
                .Build();

            // var result = await kernel.InvokePromptAsync("Hi, who are you?");

            var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
            var result = await chatCompletionService.GetChatMessageContentsAsync("Hi, who are you?",
                new PromptExecutionSettings() { ModelId = "gpt2" },
                kernel);



#pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        }
    }
}
