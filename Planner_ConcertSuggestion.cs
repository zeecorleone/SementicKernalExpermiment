
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Planning.Handlebars;
using SK_Project1.Plugins;

namespace SK_Project1;

public class Planner_ConcertSuggestion
{
    public static async Task Test(Kernel kernel)
    {
        kernel.ImportPluginFromType<MusicLibraryPlugin>();
        kernel.ImportPluginFromType<MusicConcertPlugin>();
        kernel.ImportPluginFromPromptDirectory("Prompts");

#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable SKEXP0060 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        
        var planner = new HandlebarsPlanner(new HandlebarsPlannerOptions() { AllowLoops = true });

        string location = "Redmond WA USA";
        string goal = @$"Based on the user's recently played music, suggest a concert for the user living in ${location}";

        var plan = await planner.CreatePlanAsync(kernel, goal);

        var result = await plan.InvokeAsync(kernel);
        Console.WriteLine(result + "\n\n");

#pragma warning restore SKEXP0060 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore IDE0059 // Unnecessary assignment of a value

    }
}
