
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SK_Project1.Plugins;

namespace SK_Project1;


//The Semantic Kernel SDK allows you to automatically coordinate functions and prompts
//that are referenced in your kernel. Rather than manually invoking functions and prompts,
//this tool helps you save time and makes your applications smarter. Let's try it out!

public class AutomaticallyInvokeFunction
{
    public static async Task Test(Kernel kernel)
    {
        kernel.ImportPluginFromType<MusicLibraryPlugin>();
        kernel.ImportPluginFromType<MusicConcertPlugin>();
        kernel.ImportPluginFromPromptDirectory("Prompts");

        OpenAIPromptExecutionSettings settings = new()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };

        string prompt = @"I live in Portland OR USA. Based on my recently 
        played songs and a list of upcoming concerts, which concert 
        do you recommend?";

        var result = await kernel.InvokePromptAsync(prompt, new KernelArguments(settings));

        Console.WriteLine(result + "\n\n");
    }
}
