using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TechnoApi.Models;

namespace TechnoApi.Services
{
    public class ApiService
    {
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:3000") // URL base de tu API Node
        };

        public async Task<Track?> GetItemAsync(string parameter)
        {
            // En tu API: siempre se llama a /tracks (igual que /paises en el ejemplo)

            string endpoint = $"/tracks";

            HttpResponseMessage response = await client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return null;

            string content = await response.Content.ReadAsStringAsync();

            // Tu JSON ES UN ARRAY, no un objeto con "results"

            var tracks = JsonSerializer.Deserialize<List<Track>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (tracks == null || tracks.Count == 0)
                return null;

            // Buscar por título (igual que el ejemplo buscaba por nombre)
            Track? trackFound = tracks.FirstOrDefault(t =>
                t.Title.ToLower().Contains(parameter.ToLower()));

            return trackFound;
        }
    }
}
