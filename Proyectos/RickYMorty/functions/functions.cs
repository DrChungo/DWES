using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RickYmortyApi;
using RickYmortyApi.Models;
using RickYmortyApi.Services;

namespace RickYmortyApi.Functions
{
    public static class MenuFunctions
    {
        // BUSCAR y mostrar un elemento usando el servicio
        public static async Task SearchItem(ApiService service, List<SavedItem> saved)
        {
            Console.Write("Introduce el nombre del personaje que desas buscar: ");
            string input = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No puede estar vacío.\n");
                return;
            }

            SavedItem? result = await service.GetItemAsync(input);

            if (result == null)
            {
                Console.WriteLine("No encontrado.\n");
                return;
            }

            Console.WriteLine($"Nombre: {result.Name}");
         

            Console.Write("¿Guardar en la lista? (s/n): ");
            string op = (Console.ReadLine() ?? "").Trim().ToLower();

            if (op == "s")
            {
                saved.Add(result);
                Console.WriteLine("Guardado correctamente.");
            }
            else
            {
                Console.WriteLine("No guardado.");
            }
        }

        // LISTAR elementos
        public static void ListItems(List<SavedItem> saved)
        {
            if (saved.Count == 0)
            {
                Console.WriteLine("No hay elementos guardados.");
                return;
            }

            Console.WriteLine("=== ELEMENTOS GUARDADOS ===");

            for (int i = 0; i < saved.Count; i++)
            {
                var item = saved[i];
                Console.WriteLine($"{i + 1}. {item.Name} ({item.Species} - {item.Status} )");
            }
        }
    }
}
