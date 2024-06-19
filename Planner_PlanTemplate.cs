
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Planning.Handlebars;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
using SK_Project1.Plugins;

namespace SK_Project1;

public class Planner_PlanTemplate
{
    public static async Task Test(Kernel kernel)
    {
        kernel.ImportPluginFromType<MusicLibraryPlugin>();
        kernel.ImportPluginFromType<MusicConcertPlugin>();
        kernel.ImportPluginFromPromptDirectory("Prompts");


#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable SKEXP0060 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

        //var planner = new HandlebarsPlanner(new HandlebarsPlannerOptions() { AllowLoops = true });

        //string location = "Redmond WA USA";
        //string goal = @$"Based on the user's recently played music, suggest a concert for the user living in {location}";

        //var concertPlan = await planner.CreatePlanAsync(kernel, goal);

        //Console.WriteLine("Concert Plan:");
        //Console.WriteLine(concertPlan);

        //ABOVE LINES REMOVED AFTER COPYING GENERATED 'concertPlan' text.

        var songSuggesterFunction = kernel.CreateFunctionFromPrompt(
            promptTemplate: @"Based on users's recenlty played music:
                {{$recentlyPlayedSongs}}
            
                recommend a song to the user from the music library:
                {{$musicLibrary}}",
            functionName: "SuggestSong",
            description: "Suggest a song to the user");

        kernel.Plugins.AddFromFunctions("SuggestSongPlugin", [songSuggesterFunction]);

        //var songSuggestPlan = await planner.CreatePlanAsync(kernel, @"Suggest a song from the music library to the user based on their recently played songs");

        //Console.WriteLine("Song Plan:");
        //Console.WriteLine(songSuggestPlan);

        //ABOVE LINES REMOVED AFET COPYING 'songSuggestPlan' text.


        //NOW..
        //Use handlebars plan template saved in txt file to use as plan

        string dir = Directory.GetCurrentDirectory();
        string template = File.ReadAllText($"{dir}\\Plans\\handlebarsTemplate.txt");

        var handlebarsPromptFunction = kernel.CreateFunctionFromPrompt(
            new PromptTemplateConfig()
            {
                Template = template,
                TemplateFormat = "handlebars"
            },
            new HandlebarsPromptTemplateFactory()
        );

        //Not try it
        string location = "Redmond WA USA";
        var templateResult = await kernel.InvokeAsync(handlebarsPromptFunction,
            new KernelArguments()
            {
                { "location", location },   //"location" is a variable in handlebarsTemplate.txt
                { "suggestConcert", false } //"suggestConcert" is a variable in handlebarsTemplate.txt
            });

        Console.WriteLine(templateResult + "\n\n");

#pragma warning restore SKEXP0060 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore IDE0059 // Unnecessary assignment of a value


    }
}
