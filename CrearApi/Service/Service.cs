using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using CrearApi.Models;

namespace CrearApi.Services
{
    public class ApiService
    {
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:3000") // URL base de tu API Node
        };

         public async Task<List<Track>?> GetAllTracksAsync()
        {
            string endpoint = "/tracks";

            HttpResponseMessage response = await client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return null;

            string content = await response.Content.ReadAsStringAsync();

            var tracks = JsonSerializer.Deserialize<List<Track>>(
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return tracks;
        }
    }
}
