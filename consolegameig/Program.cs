using System.Net.Http;
using System.Net.Http.Json;
using consolegameig;

HttpClient client = new HttpClient();

CatFact? catFact = await client.GetFromJsonAsync<CatFact>( "https://catfact.ninja/fact");

Console.WriteLine(catFact.Fact);
Console.WriteLine(catFact.Fact);

