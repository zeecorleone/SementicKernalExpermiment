

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SK_Project1;

public class CustomPluginFromDirectoryWithHistory
{
    public static async Task Test(Kernel kernel)
    {
        var plugins = kernel.CreatePluginFromPromptDirectory("Prompts/TravelPlugins");

        ChatHistory history = [];

        string input = @"I'm planning an anniversary trip with my spouse. We like hiking, 
        mountains, and beaches. Our travel budget is $15000";

        var result = await kernel.InvokeAsync<string>(plugins["SuggestDestinations"],
        new() { { "input", input } });

        Console.WriteLine(result + "\n\n");

        history.AddUserMessage(input);
        history.AddAssistantMessage(result);

        //Continue conversation...
        Console.WriteLine("Where would you like to go?");
        input = Console.ReadLine();

        result = await kernel.InvokeAsync<string>(
            plugins["SuggestActivities"],
            new() {
                { "history", history },
                { "destination", input },
            });
        Console.WriteLine(result + "\n\n");

    }
}
