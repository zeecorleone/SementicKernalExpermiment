using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Core;

namespace SK_Project1;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion(
            "gpt-3.5-turbo",
            Environment.GetEnvironmentVariable("OPENAI_API_KEY")!,
            "");

#pragma warning disable SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        
        //add built-in time plugin
        //builder.Plugins.AddFromType<TimePlugin>();

        //add built-in ConversationSummary plugin
        //builder.Plugins.AddFromType<ConversationSummaryPlugin>();
#pragma warning restore SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

        var kernel = builder.Build();

        ////test sk
        //var result = await kernel.InvokePromptAsync("Give me a list of breakfast foods with eggs and cheese");
        //Console.WriteLine(result + "\n\n");

        ////Test some Builtin Plugins
        //await BuiltInPlugins.Test(kernel);

        ////Test some prompt templating
        //await PromptTemplating.Test(kernel);


        ////Test prompt templating with "personas"
        //await PromptTemplatingWithPersonas.Test(kernel);

        ////Test plugins from Directory structure
        //await CustomPluginsFromDirectory.Test(kernel);

        ////Test plugins from Directory structure
        //await CustomPluginFromDirectoryWithHistory.Test(kernel);

        ////Test plugins from Directory structure
        //await CustomPluginWithNativeCode.Test(kernel);

        ////Test plugins from Directory structure
        //await CustomPluginWithPromptAndNativeCodeCombo.Test(kernel);

        ////Test plugins from Directory structure
        //await Planner_ConcertSuggestion.Test(kernel);

        //Test plugins from Directory structure
        await Planner_PlanTemplate.Test(kernel);

    }
}
