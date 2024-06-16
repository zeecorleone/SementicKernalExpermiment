

using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SK_Project1.Plugins;

public class MusicLibraryPlugin
{
    [KernelFunction, Description("Get a list of music recenlty played by the user")]
    public static string GetRecentPlays()
    {
        string dir = Directory.GetCurrentDirectory();
        string content = File.ReadAllText($"{dir}\\Plugins\\data\\recentlyplayed.txt");
        return content;
    }

    [KernelFunction, Description("Add a song to the recentl played list")]
    public static string AddToRecentlyPlayed(
        [Description("The name of the artist")] string artist,
        [Description("The title of the song")] string song,
        [Description("The song genre")] string genre)
    {
        var dir = Directory.GetCurrentDirectory();
        string filePath = $"{dir}\\Plugins\\data\\recentlyplayed.txt";
        string jsonContent = File.ReadAllText(filePath);
        
        var recentlyPlayed = (JsonArray) JsonNode.Parse(jsonContent);

        var newSong = new JsonObject()
        {
            ["title"] = song,
            ["artist"] = artist,
            ["genre"] = genre
        };

        recentlyPlayed.Insert(0, newSong);
        File.WriteAllText(filePath, JsonSerializer.Serialize(recentlyPlayed, new JsonSerializerOptions { WriteIndented = true }));

        return $"Added {song} to the recently played list";
    }

}
