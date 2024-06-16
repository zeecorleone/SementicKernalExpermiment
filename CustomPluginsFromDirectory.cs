
using Microsoft.SemanticKernel;

namespace SK_Project1;

public class CustomPluginsFromDirectory
{
    public static async Task Test(Kernel kernel)
    {
        var plugins = kernel.CreatePluginFromPromptDirectory("Prompts");
        string input = "G, C";
        
        var result = await kernel.InvokeAsync(
            plugins["SuggestChords"],
            new KernelArguments() { { "startingChords", input } }
            );

        Console.WriteLine(result + "\n\n");

        input = @"{ 'playerName': 'Johny KK', 'playerClass': 'Warlock', 'playerHealthPercentage': 81 }";
        result = await kernel.InvokeAsync(
            plugins["JsonToClass"],
            new KernelArguments() { { "json", input } });

        Console.WriteLine(result + "\n\n");

    }
}
