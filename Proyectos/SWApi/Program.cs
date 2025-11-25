using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SWApi;
using SWApi.Services;
using SWApi.Functions;

namespace SWApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service   = new ApiService();
            var savedList = new List<SavedItem>();

            string opcion;

            do
            {
                Console.WriteLine("Gestor de naves ");
                Console.WriteLine("1. Buscar");
                Console.WriteLine("2. Listar guardados");
                System.Console.WriteLine("3.Guardar en un Json");
                Console.WriteLine("0. Salir");
                Console.Write("Elige una opción: ");
                opcion = Console.ReadLine()?.Trim() ?? "";
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        await  Functions.Functions.SearchItem(service, savedList);
                        break;

                    case "2":
                        Functions.Functions.ListItems(savedList);
                        break;

                    case "3":

                    Functions.Functions.SaveListToJson(savedList);
                    break;

                    case "0":
                        Console.WriteLine("Saliendo...");
                        break;

                    default:
                        Console.WriteLine("Opción incorrecta.");
                        break;
                }

                Console.WriteLine();
            }
            while (opcion != "0");
        }
    }
}
