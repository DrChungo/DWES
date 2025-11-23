
using RickYmortyApi.Services;
using RickYmortyApi.Functions;
using RickYmortyApi.Models;

namespace RickYmortyApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service = new ApiService();
            var savedList = new List<SavedItem>();

            string opcion;

            do
            {
                Console.WriteLine("=== Buscador Rick y Morty ===");
                Console.WriteLine("1. Buscar");
                Console.WriteLine("2. Listar guardados");
                Console.WriteLine("3. Salir");
                Console.Write("Elige una opción: ");
                opcion = Console.ReadLine()?.Trim() ?? "";

                switch (opcion)
                {
                    case "1":
                        await MenuFunctions.SearchItem(service, savedList);
                        break;

                    case "2":
                        MenuFunctions.ListItems(savedList);
                        break;

                    case "3":
                        Console.WriteLine("Saliendo...");
                        break;

                    default:
                        Console.WriteLine("Opción incorrecta.");
                        break;
                }

                Console.WriteLine();
            }
            while (opcion != "3");
        }
    }
}
