namespace consolegameig;

class Program
{
    static async Task Main(string[] args)
    {
        ScoreboardApi api = new ScoreboardApi();
        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== SCOREBOARD ===");
            Console.WriteLine();
            Console.WriteLine("1. Submit Score");
            Console.WriteLine("2. View Scoreboard");
            Console.WriteLine("3. Exit");
            Console.WriteLine();
            Console.WriteLine("==================");
            Console.Write("Choose: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    await SubmitFlow(api);
                    break;
                case "2":
                    await GetFlow(api);
                    break;
                case "3":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Not a valid option.");
                    break;
            }
        }
    }

    static async Task SubmitFlow(ScoreboardApi api)
    {
        Console.WriteLine("Name: ");
        string name = Console.ReadLine();

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Name is empty.");
            return;
        }
        
        Console.WriteLine("Score: ");
        string score = Console.ReadLine();

        if (!int.TryParse(score, out int scoreNumber))
        {
            Console.WriteLine("Score Has to be a number");
        }
        
        GameData data = new GameData() { name = name, score = scoreNumber };
        bool success = await api.SubmitScore(data);
        
        Console.WriteLine(success ? "Score submitted!" : "Could not submit score.");
    }

    static async Task GetFlow(ScoreboardApi api)
    {
        Console.WriteLine("Loading scoreboard...");
        
        List<GameData> scores = await api.GetScores();

        if (scores.Count == 0)
        {
            Console.WriteLine("No scores found.");
            return;
        }
        
        List<GameData> sortedScores = scores.OrderByDescending(s => s.score).Take(10).ToList();
        
        Console.WriteLine("=== Leaderboard ===");

        for (int i = 0; i < sortedScores.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {sortedScores[i].name, -12} {sortedScores[i].score}");
        }
    }
}