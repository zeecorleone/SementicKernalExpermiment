

using Microsoft.SemanticKernel;

namespace SK_Project1;

public static class PromptTemplating
{
    public static async Task Test(Kernel kernel)
    {
        #region [Prompt Templating]
        //Try Prompt Template
        string history = @"In the heart of my bustling kitchen, I have embraced 
        the challenge of satisfying my family's diverse taste buds and 
        navigating their unique tastes. With a mix of picky eaters and 
        allergies, my culinary journey revolves around exploring a plethora 
        of vegetarian recipes.

        One of my kids is a picky eater with an aversion to anything green, 
        while another has a peanut allergy that adds an extra layer of complexity 
        to meal planning. Armed with creativity and a passion for wholesome 
        cooking, I've embarked on a flavorful adventure, discovering plant-based 
        dishes that not only please the picky palates but are also heathy and 
        delicious.";

        string dislikedItems = "pasta, butter";

        string prompt = @"This is some information about the user's background: 
        {{$history}}
        
        Given this user's background, provide a list of relevant recipes.
        
        The recipies should not contain {{$dislikes}}";

        var rs = await kernel.InvokePromptAsync(prompt,
            new KernelArguments() { { "history", history }, { "dislikes", dislikedItems } });

        Console.WriteLine(rs + "\n\n");


        //Prompt Template continued..
        var language = "Spanish";
        var traverllerHistory = "I'm traveling with my kids and one of them has a peanut allergy";
        prompt = @"Consider traverller's background: {{$traverllerHistory}}.

        create a list of helpful phrases and words in ${language} a traveller can find useful.

        Group phrases by category. Include common direction 
        words. Display the phrases in the following format: 
        Hello - Hola.

        Include any important sentences based on the provided background.";// Please do not make up anything about the traverller on your own. in your last response, you said 'traveller l'";

        var result = await kernel.InvokePromptAsync(prompt, new KernelArguments() { { "traverllerHistory", traverllerHistory } });
        Console.WriteLine(result + "\n\n");
        #endregion
    }
}
