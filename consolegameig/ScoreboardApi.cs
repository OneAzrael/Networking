using System.Net.Http.Json;
using System.Text.Json;

namespace consolegameig;

public class ScoreboardApi
{
    HttpClient client = new HttpClient();
    
    private const string PostUrl = "https://hooks.zapier.com/hooks/catch/8338993/ujs9jj9/";
    private const string GetUrl = "https://script.google.com/macros/s/AKfycbys5aEPMvNCutyhNYYCcQcCjzsi2UtqNspmKyCH-AicJxJbCJMrAoT0LUaYaXhTWA8n/exec";
    
    public async Task<List<GameData>> GetScores()
    {
        try
        {
            List<GameData>? scores = await client.GetFromJsonAsync<List<GameData>>(GetUrl);
            return scores ?? new List<GameData>();
        }
        catch (HttpRequestException)
        {
            Console.WriteLine("Could not connect to server, please try again");
            return new List<GameData>();
        }
        catch (JsonException)
        {
            Console.WriteLine("The server sent something that isn't readable");
            return new List<GameData>();
        }
    }

    public async Task<bool> SubmitScore(GameData Entry)
    {
        try
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(PostUrl, Entry);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException)
        {
            Console.WriteLine("Could not connect to server, please try again");
            return false;
        }
    }
} 