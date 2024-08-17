using AITravelAgent.Plugins.ConvertCurrency;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.Core;
using System.Text;

namespace AITravelAgent;

internal class Program
{
    public static async Task Main22(string[] args)
    {
        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion(
           "gpt-3.5-turbo",
           Environment.GetEnvironmentVariable("OPENAI_API_KEY")!,
           "");

        var kernel = builder.Build();

        kernel.ImportPluginFromType<CurrencyConverter>();
#pragma warning disable SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        kernel.Plugins.AddFromType<ConversationSummaryPlugin>();
#pragma warning restore SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
       
        var prompts = kernel.ImportPluginFromPromptDirectory("Prompts");



        ////////////////////////////////////////////////////////
        ///
        #region [Test CurrencyConverter Plugin] 
        ////Test currency Converter plugin
        //var result = await kernel.InvokeAsync("CurrencyConverter",
        //    "ConvertAmount",
        //    new KernelArguments()
        //    {
        //        {"targetCurrencyCode", "USD"},
        //        {"amount", "52000"},
        //        {"baseCurrencyCode", "VND"}
        //    });

        //Console.WriteLine(result);

        #endregion;

        #region[Test GetTargetCurrencyPrompt]
        ////Test GetTargetCurrencyPrompt 
        //var promptResult = await kernel.InvokeAsync(
        //    prompts["GetTargetCurrencies"],
        //    new KernelArguments() { { "input", "How many Australian Dollars is 140,000 Korean Won worth?" } }
        //    );

        //Console.Write(promptResult);
        #endregion;
        ///
        /////////////////////////////////////////////////
        string input = string.Empty;
        StringBuilder chatHistory = new StringBuilder();
        do
        {
            Console.WriteLine("Hi, What would you like to do?" + "\n\n>>");
            input = Console.ReadLine();


            var intent = await kernel.InvokeAsync<string>(
            prompts["GetIntent"],
            new KernelArguments()
            {
                {"input", input }
            });

            #region [Handle Manually, using Switch statement, without "Automatic" function invoking]

            //switch (intent)
            //{
            //    case "ConvertCurrency":
            //        {
            //            //Get currency text in fomat target|base|ammount using our GetTargetCurrencies prompt
            //            var currencyText = await kernel.InvokeAsync<string>(
            //                prompts["GetTargetCurrencies"],
            //                new() { { "input", input } }
            //            );

            //            //pass the GetTargetCurrencies text to next plugin, currency converter
            //            var currencyInfo = currencyText.Split("|");
            //            var result = await kernel.InvokeAsync("CurrencyConverter",
            //                "ConvertAmount",
            //                new KernelArguments()
            //                {
            //                    {"targetCurrencyCode", currencyInfo[0]},
            //                    {"baseCurrencyCode", currencyInfo[1]},
            //                    {"amount", currencyInfo[2]},
            //                });

            //            Console.WriteLine(result + "\n\n");
            //            break;
            //        }

            //    case "SuggestDestinations":
            //        {
            //            var result = await kernel.InvokeAsync(
            //                prompts["SuggestDestinations"],
            //                new KernelArguments() { { "input", input } });

            //            break;
            //        }

            //    //. . . . .SImilarly, add other cases like:
            //    //case "SuggestActivities"
            //    //case "HelpfulPhrases"
            //    //case "Translate" 
            //    //.... TODO


            //    default:
            //        Console.WriteLine("Other/Unknown intent detected");
            //        break;


            //};

            #endregion;

            #region [Use Automatic Function Invoking, instead of Switch case for each type of intent (as did above)]

            OpenAIPromptExecutionSettings settings = new()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            switch (intent)
            {
                case "ConvertCurrency":
                // ...Code you entered previously...
                //break;
                case "SuggestDestinations":

                    chatHistory.AppendLine("User: " + input);
                    var recommendations = await kernel.InvokePromptAsync(input!);
                    Console.WriteLine(recommendations + "\n\n");

                    break;
                case "SuggestActivities":

                    var chatSummary = await kernel.InvokeAsync(
                        "ConversationSummaryPlugin",
                        "SummarizeConversation",
                        new() { { "input", chatHistory.ToString() } });

                    var activites = await kernel.InvokePromptAsync(input,
                        new KernelArguments()
                        {
                            {"input", input},
                            {"history", chatSummary},
                            {"ToolCallBehavior", ToolCallBehavior.AutoInvokeKernelFunctions}
                        });

                    chatHistory.AppendLine("User: " + input);
                    chatHistory.AppendLine("Assistant: " + activites.ToString());
                    Console.WriteLine(activites + "\n\n");
                    break;

                case "HelpfulPhrases":
                case "Translate":
                    var autoInvokeResult = await kernel.InvokePromptAsync(input!, new(settings));
                    Console.WriteLine(autoInvokeResult + "\n\n");
                    break;
                default:
                    Console.WriteLine("Sure, I can help with that.");
                    var otherIntentResult = await kernel.InvokePromptAsync(input!, new(settings));
                    Console.WriteLine(otherIntentResult + "\n\n");
                    break;
            }

            #endregion

        }
        while (!string.IsNullOrWhiteSpace(input));

        


    }
}
