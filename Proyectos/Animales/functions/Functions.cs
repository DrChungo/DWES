using System.Runtime.Intrinsics.Arm;
using System.Text.Json.Serialization;
using AnimalsApi.Models;
using AnimalsApi.Services;
namespace AnimalsApi.Funcions;


public static class Functions
{
    //Al llamrlo usaremos await, ya quer es una funcion async
    public static async Task SeachBreed(DogApiService service, List<DogBred> saveBreed)
    {

        System.Console.WriteLine("Introduce la raza");
        string breed = Console.ReadLine()?.Trim() ?? "";

        //validacion
        if (string.IsNullOrWhiteSpace(breed))
        {
            Console.WriteLine("La raza no puede estar vacía.\n");
            return;
        }


        DogBred? result = await service.GetRazaAsync(breed);

        //validacion
        if (result == null)
        {
            Console.WriteLine("Raza no encontrada.\n");
            return;
        }

        //Lo mostramos en pantralla
        Console.WriteLine($"Raza: {result.Name}");
        Console.WriteLine($"Imagen: {result.ImageUrl}");
        Console.WriteLine();

        Console.Write("¿Quieres guardarla en tu lista? (s/n): ");
        string respuesta = (Console.ReadLine() ?? "").Trim().ToLower();

        if (respuesta == "s")
        {
            // Evitar duplicados simples
            bool exist = saveBreed.Exists(r =>
                r.Name.Equals(result.Name, StringComparison.OrdinalIgnoreCase));

            if (exist)
            {
                Console.WriteLine("Esa raza ya está guardada.");
            }
            else
            {
                saveBreed.Add(result);
                Console.WriteLine("Raza guardada correctamente.");
            }
        }
        else
        {
            Console.WriteLine("Raza no guardada.");
        }
    }


    public static void listBreed(List<DogBred> saveBreed)
    {
        if (saveBreed.Count == 0)
        {
            System.Console.WriteLine("Lista vacia");
            return;
        }
        System.Console.WriteLine("Razas Guardadas");
        for (int i = 0; i < saveBreed.Count; i++)
        {
            var b = saveBreed[i];

            System.Console.WriteLine($"{i + 1}. {b.Name} -> {b.ImageUrl}");
        }


    }




}



