
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace SK_Project1.Plugins;

public class MusicConcertPlugin
{
    [KernelFunction, Description("Get a list of upcoming concerts")]
    public static string GetTours()
    {
        var dir = Directory.GetCurrentDirectory();
        string filePath = $"{dir}\\Plugins\\data\\concertdates.txt";
        string content = File.ReadAllText(filePath);
        return content;
    }
}
