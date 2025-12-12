using System;
using System.Threading.Tasks;
using CrearApi.Services;

namespace CrearApi.Functions
{
    /**
     * Muestra el listado de canciones disponibles y permite eliminar
     * una canción introduciendo su ID. El usuario puede cancelar
     * la operación introduciendo 0.
     *
     * @return Task que representa la operación asíncrona de borrado
     */
    public static class FunctionsDelete
    {
        /**
         * Ejecuta el proceso de eliminación de una canción:
         * muestra las canciones disponibles, solicita el ID
         * y realiza la llamada a la API para eliminarla.
         *
         * @return Task que representa la operación asíncrona
         */
        public static async Task DeleteSongAsync()
        {
            Console.Clear();

       
            await FunctionsList.ShowAllTracksAsync();

            Console.WriteLine("\n=== ELIMINAR CANCIÓN ===");
            Console.WriteLine("Pulsa 0 para cancelar\n");
            Console.Write("ID a eliminar: ");

            string? input = Console.ReadLine();

        
            if (input == "0")
            {
                Console.WriteLine("\nOperación cancelada.");
                return;
            }
          
            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("\nID no válido.");
                return;
            }

           
            var api = new ApiService();
            bool ok = await api.DeleteTrackAsync(id);

            // Mostrar resultado
            Console.WriteLine(ok
                ? $"\nCanción eliminada correctamente (ID {id})."
                : $"\nNo se pudo eliminar (¿existe el ID {id}?).");
        }
    }
}
