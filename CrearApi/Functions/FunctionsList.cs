using System;
using System.Threading.Tasks;
using CrearApi.Services;

namespace CrearApi.Functions
{
    /**
     * Contiene funciones relacionadas con la visualización
     * de información de las canciones.
     */
    public static class FunctionsList
    {
        /**
         * Obtiene todas las canciones desde la API y muestra por consola el ID y el título de cada una.
         *
         * @return Task que representa la operación asíncrona de listado
         */
        public static async Task ShowAllTracksAsync()
        {
            Console.Clear();
            Console.WriteLine("=== LISTADO DE CANCIONES ===\n");

            var api = new ApiService();
            var tracks = await api.GetAllTracksAsync();

           
            if (tracks == null || tracks.Count == 0)
            {
                Console.WriteLine("No hay canciones disponibles.");
                return;
            }

          
            foreach (var track in tracks)
            {
                Console.WriteLine($"ID: {track.Id} | {track.Title}");
            }
        }
    }
}
