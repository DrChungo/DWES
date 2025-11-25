using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SWApi;

namespace SWApi.Services
{
    public class ApiService
    {
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://swapi.dev")
        };

        public async Task<SavedItem?> GetItemAsync(string parameter)
        {
            // Ejemplo Rick&Morty: /api/character/?name={nombre}
            string endpoint = $"/api/starships/?search={parameter.ToLower()}";

            HttpResponseMessage response = await client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return null;

            string content = await response.Content.ReadAsStringAsync();

            ApiResponse? apiResponse = JsonSerializer.Deserialize<ApiResponse>(content);

            if (apiResponse == null || apiResponse.Results == null || apiResponse.Results.Count == 0)
                return null;

            //cremos el  objeto para luego guardarlo
            Starships starships = apiResponse.Results.First();

            return new SavedItem
            {
                Name   = starships.Name,
                Model  = starships.Model,
                Manufacturer = starships.Manufacturer,
                Speed = starships.Speed,
                Crew= starships.Crew
            };
        }
    }
}
