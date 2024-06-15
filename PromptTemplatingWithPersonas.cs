
using Microsoft.SemanticKernel;

namespace SK_Project1;

internal class PromptTemplatingWithPersonas
{
    public static async Task Test(Kernel kernel)
    {
        string language = "Spanish";
        string history = @"I'm traveling with my kids and one of them has a peanut allergy.";

        //Assign a persona to the prompt
        string prompt = @"
        You are a travel assistant. You are helpful, creative, and very friendly.
        Consider traveler's background: {{$history}}.
        
        Create a list of helpful phrases and words in {{$language}} a traveler would find useful.
        
        Group phrases by category. Include common direction words. 
        Display the phrases in the following format: 
        Hello - Hola

        Begin with: 'Here are some phrases in {{$language}} you may find helpful:' 
        and end with: 'I hope this helps you on your trip!'
        ";

        var result = await kernel.InvokePromptAsync(prompt,
            new KernelArguments() { { "history", history }, { "language", language } });


        Console.WriteLine(result);

    }
}
