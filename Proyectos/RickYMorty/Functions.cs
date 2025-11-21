using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RickApp{
static class Function
{
    public static async Task EjecutarMenu()
    {
        bool salir = false;

        while (!salir)
        {
            MostrarMenu();

            Console.Write("Elige una opción: ");
            string? opcion = Console.ReadLine();
            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    await BuscarYGuardarPersonaje();
                    break;

                case "2":
                    ListarPersonajes();
                    break;

                case "3":
                    salir = true;
                    Console.WriteLine("Saliendo... ¡Hasta otra dimensión!");
                    break;

                default:
                    Console.WriteLine("Opción inválida.\n");
                    break;
            }
        }
    }

    private static void MostrarMenu()
    {
        Console.WriteLine("\n==== MINI GESTOR RICK & MORTY ====");
        Console.WriteLine("1. Buscar personaje por nombre");
        Console.WriteLine("2. Listar personajes guardados");
        Console.WriteLine("3. Salir");
    }

    private static async Task BuscarYGuardarPersonaje()
    {
        Console.Write("Introduce el nombre del personaje: ");
        string? nombre = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("Nombre vacío.");
            return;
        }
        
        //LLamo a la funcion de la api que los conecta
        var json = await Api.GetCharacterAsync(nombre);

        if (json == null)
        {
            Console.WriteLine("Personaje no encontrado.");
            return;
        }

        using (json)
        {
            JsonElement root = json.RootElement;

            JsonElement results = root.GetProperty("results");

            if (results.GetArrayLength() == 0)
            {
                Console.WriteLine("Personaje no encontrado.");
                return;
            }

            JsonElement pj = results[0];
         



            string nombreP = pj.GetProperty("name").GetString() ?? "Desconocido";
            string estado = pj.GetProperty("status").GetString() ?? "Unknown";
            string especie = pj.GetProperty("species").GetString() ?? "Unknown";
            string genero = pj.GetProperty("gender").GetString() ?? "Unknown";

            Console.WriteLine($"Nombre: {nombreP}");
            Console.WriteLine($"Estado: {estado}");
            Console.WriteLine($"Especie: {especie}");
            Console.WriteLine($"Género: {genero}");
            Console.WriteLine();

            Console.Write("¿Quieres guardarlo? (s/n): ");
            string? resp = Console.ReadLine()?.Trim().ToLower();

            if (resp == "s")
            {
                Data.PersonajesGuardados.Add(new Personaje
                {
                    Nombre = nombreP,
                    Estado = estado,
                    Especie = especie,
                    Genero = genero
                });

                Console.WriteLine("Guardado con éxito.");
            }
            else
            {
                Console.WriteLine("No guardado.");
            }
        }
    }

    private static void ListarPersonajes()
    {
        if (Data.PersonajesGuardados.Count == 0)
        {
            Console.WriteLine("No tienes personajes guardados todavía.");
            return;
        }

        Console.WriteLine("=== MIS PERSONAJES ===");
        for (int i = 0; i < Data.PersonajesGuardados.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Data.PersonajesGuardados[i]}");
        }
    }
}}
