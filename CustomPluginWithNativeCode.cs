
using Microsoft.SemanticKernel;
using SK_Project1.Plugins;

namespace SK_Project1;

public class CustomPluginWithNativeCode
{
    public static async Task Test(Kernel kernel)
    {
        kernel.ImportPluginFromType<MusicLibraryPlugin>();

        var result = await kernel.InvokeAsync(
            "MusicLibraryPlugin",
            "AddToRecentlyPlayed",
            new()
            {
                { "artist", "Johny KK" },
                { "song", "NewSong!" },
                { "genre", "Pop" }
            });

        Console.WriteLine(result + "\n\n");

        result = await kernel.InvokeAsync(
             "MusicLibraryPlugin",
            "GetRecentPlays");
        
        Console.WriteLine(result + "\n\n");
    }
}
