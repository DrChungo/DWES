using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RickYmortyApi;
using RickYmortyApi.Models;

namespace RickYmortyApi.Services
{
    public class ApiService
    {
        // Cliente HTTP con la URL base de la API
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://rickandmortyapi.com")
        };

        // ---------------------------------------------------------
        // FUNCIÓN GET
        // Llama al endpoint y devuelve un objeto de tu programa.
        // ---------------------------------------------------------
        public async Task<SavedItem?> GetItemAsync(string parameter)
        {
            // Endpoint completo (cambia según tu API)
            string endpoint = $"/api/character/?name={parameter.ToLower()}";

            HttpResponseMessage response = await client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return null;

            string content = await response.Content.ReadAsStringAsync();

            // Convertimos el JSON REAL → ApiResponse
            ApiResponse? apiResponse = JsonSerializer.Deserialize<ApiResponse>(content);


            //validacion
            if (apiResponse == null)
                return null;

            //Creamos el objeto para usarlo

            Character character = apiResponse.Results.First();

            // Convertimos ApiResponse → SavedItem
            
            return new SavedItem
            {
                Name = character.Name,
                Gender = character.Gender,
                Species = character.Species,
                Status = character.Status
            };
        }
    }
}
