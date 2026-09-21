using System.Net.Http.Json;
using consolegameig;

HttpClient client = new HttpClient();

string apiUrl = "https://catfact.ninja/fact";
string databaseUrl = "https://hooks.zapier.com/hooks/catch/8338993/ujs9jj9/";
string webhookUrl = "https://webhook.site/ebf18532-63a8-4b0b-a1d2-e53b3e5cec1b";
string scoreboardUrl = "https://script.google.com/macros/s/AKfycbys5aEPMvNCutyhNYYCcQcCjzsi2UtqNspmKyCH-AicJxJbCJMrAoT0LUaYaXhTWA8n/exec";
HttpResponseMessage response;

int totalLength = 0;




//-- Get section --
for (int i = 1; i <= 5; i++)
{
    CatFact? catFact = await client.GetFromJsonAsync<CatFact>(apiUrl);

    Console.WriteLine($"Cat Fact: number #{i}");
    Console.WriteLine(catFact?.Fact);
    Console.WriteLine();
    
    totalLength += catFact?.Length ?? 0;
}

Console.WriteLine($"Fun fact these 5 facts are {totalLength} Characters long :)");




//-- Post section --
PostData data = new PostData
{
    name = "Azrael",
    health = 100,
    level = 5,
    alive = true
};

response = await client.PostAsJsonAsync(webhookUrl, data);

Console.WriteLine(response.StatusCode);
Console.WriteLine(response.IsSuccessStatusCode);



//-- Post game data Section --
/*
 
Console.WriteLine("Please enter your name:");
string name = Console.ReadLine() ?? "";

Console.WriteLine("Please enter your score:");
string? scoreInput = Console.ReadLine();

if (int.TryParse(scoreInput, out int score))
{
    GameData gameData = new GameData
    {
        name = name,
        score = score
    };
    
    Console.WriteLine("Submitting");
    response = await client.PostAsJsonAsync(databaseUrl, gameData);
    Console.WriteLine(response.IsSuccessStatusCode ? "Score Submitted Successfully" : $"Score Submitted Failed {response.StatusCode}");
}
else
{
    Console.WriteLine("Score needs to be a number!");
}

*/

//-- Get game data Section --
    List<GameData> scores = await client.GetFromJsonAsync<List<GameData>>(scoreboardUrl);

    if (scores != null)
    {
        foreach (GameData entry in scores)
        {
            Console.WriteLine($"{entry.name}: {entry.score}");
        }
    }
    
    var sortedScores = scores.OrderByDescending(x => x.score);
    var topTen = scores.OrderByDescending(x => x.score).Take(10);
    Console.WriteLine("=== LEADERBOARD ===");
    foreach (var entry in topTen)
    {
        Console.WriteLine($"{entry.name}: {entry.score}");
    }
