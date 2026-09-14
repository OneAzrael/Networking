using System.Net.Http;
using System.Net.Http.Json;
using consolegameig;

HttpClient client = new HttpClient();

int totalLength = 0;

for (int i = 1; i <= 5; i++)
{
    CatFact? catFact = await client.GetFromJsonAsync<CatFact>("https://catfact.ninja/fact");

    Console.WriteLine($"Cat Fact: number #{i}");
    Console.WriteLine(catFact.Fact);
    Console.WriteLine();
    
    totalLength += catFact?.Length ?? 0;
}

Console.WriteLine($"Fun fact these 5 facts are {totalLength} Characters long :)");