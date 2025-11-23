using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using <<<CAMBIAR: NAMESPACE >>>;
using <<<CAMBIAR: NAMESPACE >>>.Services;

namespace <<<CAMBIAR: NAMESPACE >>>.Functions
{
    public static class Functions
    {
        public static async Task SearchItem(ApiService service, List<SavedItem> savedList)
        {
            Console.Write("Introduce <<<CAMBIAR: TEXTO (ej: nombre del personaje) >>>: ");
            string input = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No puede estar vacío.\n");
                return;
            }

            SavedItem? item = await service.GetItemAsync(input);

            if (item == null)
            {
                Console.WriteLine("No encontrado.\n");
                return;
            }

            Console.WriteLine("=== RESULTADO ===");
            Console.WriteLine($"Nombre : {item.Name}");
            Console.WriteLine($"Estado : {item.State}");
            Console.WriteLine($"Especie: {item.Specie}");
            Console.WriteLine($"Género: {item.Gender}");
            Console.WriteLine();

            Console.Write("¿Guardar en la lista? (s/n): ");
            string op = (Console.ReadLine() ?? "").Trim().ToLower();

            if (op == "s")
            {
                savedList.Add(item);
                Console.WriteLine("Guardado.");
            }
            else
            {
                Console.WriteLine("No guardado.");
            }
        }

        public static void ListItems(List<SavedItem> savedList)
        {
            if (savedList.Count == 0)
            {
                Console.WriteLine("No hay elementos guardados.");
                return;
            }

            Console.WriteLine("=== ELEMENTOS GUARDADOS ===");
            for (int i = 0; i < savedList.Count; i++)
            {
                var p = savedList[i];
                Console.WriteLine($"{i + 1}. {p.Name} | {p.State} | {p.Specie} | {p.Gender}");
            }
        }
    }
}
