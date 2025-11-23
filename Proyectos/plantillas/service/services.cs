using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using <<<CAMBIAR: NAMESPACE >>>;

namespace <<<CAMBIAR: NAMESPACE >>>.Services
{
    public class ApiService
    {
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("<<<CAMBIAR: URL_BASE_API (ej: https://rickandmortyapi.com) >>>")
        };

        public async Task<SavedItem?> GetItemAsync(string parameter)
        {
            // Ejemplo Rick&Morty: /api/character/?name={nombre}
            string endpoint = $"<<<CAMBIAR: ENDPOINT CON {parameter} >>>";

            HttpResponseMessage response = await client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return null;

            string content = await response.Content.ReadAsStringAsync();

            ApiResponse? apiResponse = JsonSerializer.Deserialize<ApiResponse>(content);

            if (apiResponse == null || apiResponse.Results == null || apiResponse.Results.Count == 0)
                return null;

//cremos el  objeto para luego guardarlo
            Character character = apiResponse.Results.First();

            return new SavedItem
            {
                Name   = character.Name,
                State  = character.Status,
                Specie = character.Species,
                Gender = character.Gender
            };
        }
    }
}
