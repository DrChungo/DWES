using System;
using System.Threading.Tasks;
using CrearApi.Functions;

namespace CrearApi
{
    internal class Program
    {

            /**
            Menu del juego
            */
        static async Task Main(string[] args)
        {
            string? option;

            do
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ PRINCIPAL ===");
                Console.WriteLine("1. Jugar");
                Console.WriteLine("2. Añadir canción");
                Console.WriteLine("3. Eliminar canción");
                Console.WriteLine("4. Listar canciones");
                Console.WriteLine("0. Salir");
                Console.Write("\nElige una opción: ");

                option = Console.ReadLine()?.Trim();

                switch (option)
                {
                    case "1":
                        await GameFunctions.RunAsync();
                        break;

                    case "2":
                        await FunctionsAdd.AddSongAsync();
                        break;

                    case "3":
                        await FunctionsDelete.DeleteSongAsync();
                        break;

                    case "4":
                        await FunctionsList.ShowAllTracksAsync();
                        break;

                    case "0":
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intenta de nuevo.");
                        break;
                }


                if (option != "0")
                {
                    Console.WriteLine("\nPulsa cualquier tecla para volver al menú...");
                    Console.ReadKey();
                }

            } while (option != "0");
        }
    }
}
