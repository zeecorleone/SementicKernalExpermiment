
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


        Console.WriteLine(result + "\n\n");

        //test personas with roles
        string prompt2 = @"
         The following is a conversation with an AI travel assistant. 
         The assistant is helpful, creative, and very friendly. Always responds in respectful and professional tone.

        <message role=""user"">Can you give me some travel destination suggestions?</message>
        <message role=""assistant"">Of course! Do you have a budget or any specific activites in mind?</message>
        <message role=""user"">{{$input}}</message>
        ";

        string input = "I'm planning an anniversary trip with my spouse. We like hiking, mountains," +
                        "and beaches. Our travel budget is $15000";


        result = await kernel.InvokePromptAsync(prompt2, 
            new KernelArguments() { { "input", input } });

        Console.WriteLine(result + "\n\n");


        //format response
        string prompt3 = @"
        <message role=""system"">Instructions: Identify the from and to destinations 
        and dates from the user's request</message>
        
        <message role=""user"">Can you give me a list of flights from Seattle to Tokyo? 
        I want to travel from March 11 to March 18.</message>
        
        <message role=""assistant"">Seattle|Tokyo|03/11/2024|03/18/2024|Turkish Airlines</message>
        
        <message role=""user"">{{$input}}</message>";

        input = @"I have a vacation from June 1 to July 22. I want to go to Greece. 
        I live in Chicago.";

        result = await kernel.InvokePromptAsync(prompt3,
            new KernelArguments() { { "input", input } });

        Console.WriteLine(result + "\n\n");
    }
}
