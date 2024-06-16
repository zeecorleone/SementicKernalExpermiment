
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using SK_Project1.Plugins;

namespace SK_Project1;

public class CustomPluginWithPromptAndNativeCodeCombo
{
    public static async Task Test(Kernel kernel)
    {
        kernel.ImportPluginFromType<MusicLibraryPlugin>();

        string prompt = @"This is the list of music avaialble to the user:
        {{MusicLibraryPlugin.GetMusicLibrary}}

        This is a list of music user has recently played:
        {{MusicLibraryPlugin.GetRecentPlays}}

        Based on their recently played music, suggest a song from the list to play next.
        ";

        var result = await kernel.InvokePromptAsync(prompt);

        Console.WriteLine(result + "\n\n");


        ///////////////TRY CHatting based on history///////////////////////
       //exprement only, not working 100% as expected

        ChatHistory chatHistory = [];
        chatHistory.AddSystemMessage("You are a helpful assistant, answering user questions. If anything is not clear, you ask user again to clarify.");
        var input = "";
        Console.WriteLine("please ask any questions about your music library.");
        while(true)
        {
            Console.WriteLine("ask>> ");
            input = Console.ReadLine();
            if (input == "end")
                break;

            var inputPrompt = @"
                This is the list of songs available to user:
                {{MusicLibraryPlugin.GetMusicLibrary}}

                Consider the chat history for background:
                {{$chatHistory}}, respond to the following sentence from user:
                {{$question}}
                ";
           
            var answer = await kernel.InvokePromptAsync<string>(inputPrompt, new KernelArguments()
            {
                { "chatHistory", chatHistory },
                { "question", input }
            });

            chatHistory.AddUserMessage(input);
            chatHistory.AddAssistantMessage(answer);

            Console.WriteLine(answer + "\n\n");

        }
    }
}
