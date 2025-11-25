using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SWApi;
using SWApi.Services;
using System.IO;
using System.Text.Json;


namespace SWApi.Functions
{
    public static class Functions
    {
        public static async Task SearchItem(ApiService service, List<SavedItem> savedList)
        {
            Console.Write("Introduce el nombre de la nave");
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
            Console.WriteLine($"Model : {item.Model}");
            Console.WriteLine($"fabricantre: {item.Manufacturer}");
            Console.WriteLine($"Velocidad: {item.Speed}");
            Console.WriteLine($"Escudron: {item.Crew}");
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
                Console.WriteLine($"{i + 1}. {p.Name} | {p.Model} | {p.Manufacturer} | {p.Speed} | {p.Crew}");
            }
        }

        public static void SaveListToJson(List<SavedItem> list)
        {
            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText("naves.json", json);

            Console.WriteLine("Archivo JSON guardado correctamente.");
        }
    }
}
