

using Microsoft.SemanticKernel;

namespace SK_Project1;

public static class BuiltInPlugins
{
    public static async Task Test(Kernel kernel)
    {
        #region [Time Plugin]
        //try plugin call
        var currentDay = await kernel.InvokeAsync("TimePlugin", "DayOfWeek");
        Console.WriteLine(currentDay + "\n\n");

        const string promptTemplate = @"
            Today is: {{TimePlugin.Date}}
            Current time is: {{TimePlugin.Time}}

            Answer to the following questions using JSON syntax, including the data used.
            Is it morning, afternoon, evening, or night (morning/afternoon/evening/night)?
            Is it weekend time (weekend/not weekend)?";

        var results = await kernel.InvokePromptAsync(promptTemplate);
        #endregion


        #region [ConversationSummaryP lugin]
        //try conversation summary plugin
        var input = @"I'm a vegan in search of new recipes. I love spicy food! 
        Can you give me a list of breakfast recipes that are vegan friendly?";

        var actionItmes = await kernel.InvokeAsync(
            "ConversationSummaryPlugin",
            "SummarizeConversation", //GetConversationActionItems --> not working as expected
            new() { { "input", input } });


        Console.WriteLine(actionItmes + "\n\n");
        #endregion
    }
}
