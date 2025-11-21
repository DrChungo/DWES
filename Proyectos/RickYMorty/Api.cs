using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RickApp
{
static class Api
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task<JsonDocument?> GetCharacterAsync(string name)
    {
        try
        {
            string encoded = Uri.EscapeDataString(name.ToLower());

            var response = await client.GetAsync( $"https://rickandmortyapi.com/api/character/?name={name.ToLower()}");

            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonDocument.Parse(content);
        }
        catch
        {
            return null;
        }
    }
}
}