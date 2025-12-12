using System;
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
            BaseAddress = new Uri("http://localhost:3000")
        };

        public async Task<List<Track>?> GetAllTracksAsync()
        {
            string endpoint = "/tracks";

            try
            {
                HttpResponseMessage response = await client.GetAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"La API devolvió un código de estado no exitoso: {(int)response.StatusCode}");
                    return null;
                }

                string content = await response.Content.ReadAsStringAsync();

                var tracks = JsonSerializer.Deserialize<List<Track>>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return tracks;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error al conectar con la API Node:");
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
