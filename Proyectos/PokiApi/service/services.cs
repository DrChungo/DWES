using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PokeApi;

namespace PokeApi.Services
{
    public class ApiService
    {
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://pokeapi.co")
        };

        public async Task<SavedItem?> GetItemAsync(string parameter)
        {
            // Ejemplo Rick&Morty: /api/character/?name={nombre}

            string endpoint = $"/api/v2/pokemon/{parameter.ToLower()}";

            HttpResponseMessage response = await client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return null;

            string content = await response.Content.ReadAsStringAsync();

            Pokemon? apiResponse = JsonSerializer.Deserialize<Pokemon>(content);

            if (apiResponse == null )
                return null;


            //cremos el  objeto para luego guardarlo ()
            

            return new SavedItem
            {
                Name   = apiResponse.Name,
                Id  = apiResponse.Id,
                Type = apiResponse.Types.First().Type.Name,
                Weight = apiResponse.Weight,
                Height= apiResponse.Height

            };
        }
    }
}
