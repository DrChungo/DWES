using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RickApp
{
static class Api
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task<Personaje?> GetCharacterAsync(string name)
    {
        try
        {
            string encoded = Uri.EscapeDataString(name.ToLower());

            var response = await client.GetAsync( $"https://rickandmortyapi.com/api/character/?name={name.ToLower()}");

            if (!response.IsSuccessStatusCode) return null;

            var contentString = await response.Content.ReadAsStringAsync();
            
            // deserialize lo convierte en objeto
            
            var content=JsonSerializer.Deserialize<Personaje>(contentString);
            return content;
        }
        catch
        {
            return null;
        }
    }
}
}