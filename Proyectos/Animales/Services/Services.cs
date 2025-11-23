using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AnimalsApi.Models;

namespace AnimalsApi.Services
{
    public class DogApiService
    {
        // -------------------------------------------------------------
        // 🌐 Cliente Http con la URL base de la API de perros
        // -------------------------------------------------------------
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://dog.ceo/")
        };

        // -------------------------------------------------------------
        // 📌 FUNCIÓN GET → Obtener información de una raza
        //
        // NOTA IMPORTANTE:
        // ⭐ Antes tenías "DogApiResponse" como valor de retorno y eso causaba error
        //   porque al final de la función DEVOLVÍAS un DogBred.
        //
        // ⭐ Ahora devuelve "DogBred?" (la clase que tú usas y guardas en tu lista),
        //   así desaparece el error "Cannot implicitly convert type".
        // -------------------------------------------------------------
        public async Task<DogBred?> GetRazaAsync(string breed)
        {
            // ---------------------------------------------------------
            // Construimos el endpoint usando la raza introducida.
            // Aquí hay que poner la ruta EXACTA que usa la Dog API.
            //
            // Ejemplo:
            // https://dog.ceo/api/breed/hound/images/random
            // ---------------------------------------------------------
            string endpoint = $"api/breed/{breed.ToLower()}/images/random";

            // Petición GET a la API
            HttpResponseMessage response = await client.GetAsync(endpoint);

            // Si la API responde con error (404 → raza no existe), devolvemos null
            if (!response.IsSuccessStatusCode)
                return null;

            // Leemos la respuesta JSON como texto
            string content = await response.Content.ReadAsStringAsync();

            // ---------------------------------------------------------
            // 1️⃣ Primero deserializamos a DogApiResponse,
            //     que representa EXACTAMENTE lo que devuelve la API:
            //
            //     {
            //        "message": "URL",
            //        "status": "success"
            //     }
            //
            // 2️⃣ NO podemos deserializar directamente a DogBred porque
            //     ese JSON no tiene "Name" ni "ImageUrl".
            // ---------------------------------------------------------
            DogApiResponse? apiResponse =
                JsonSerializer.Deserialize<DogApiResponse>(content);

            // Si la API devuelve error dentro del JSON → no válido
            if (apiResponse == null || apiResponse.Status != "success")
                return null;

            // ---------------------------------------------------------
            // 3️⃣ Ahora convertimos DogApiResponse → DogBred
            //
            // Esta es la clase que tú usas para guardar razas en tu lista.
            // ---------------------------------------------------------
            return new DogBred
            {
                Name = breed.ToLower(),
                ImageUrl = apiResponse.Message
            };
        }

        // -------------------------------------------------------------
        // ❌ La Dog API NO permite POST, así que esta función no se usa.
        //     (bien que la tengas comentada)
        // -------------------------------------------------------------
        /*
        public async Task<string> CreateTodoAsync()
        {
            ...
        }
        */
    }
}
