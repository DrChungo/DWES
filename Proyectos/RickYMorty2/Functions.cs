using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RickApp
{
    static class Function
    {
        // Ejecuta el menú principal del programa Rick & Morty.
        // @param none   No recibe parámetros.
        // @return       Ejecuta un menú interactivo hasta que el usuario decida salir.
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

        // Muestra el menú en consola.
        // @param none   No recibe parámetros.
        // @return       Imprime las opciones disponibles.
        private static void MostrarMenu()
        {
            Console.WriteLine("\n==== MINI GESTOR RICK & MORTY ====");
            Console.WriteLine("1. Buscar personaje por nombre");
            Console.WriteLine("2. Listar personajes guardados");
            Console.WriteLine("3. Salir");
        }

        // Busca un personaje en la API por nombre, muestra sus datos y permite guardarlo.
        // @param none   No recibe parámetros. Solicita el nombre por consola.
        // @return       No devuelve valor; muestra datos y opcionalmente guarda el personaje.
        private static async Task BuscarYGuardarPersonaje()
        {
            Console.Write("Introduce el nombre del personaje: ");
            string? nombreUser = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombreUser))
            {
                Console.WriteLine("Nombre vacío.");
                return;
            }

            // Llama a la API para obtener datos del personaje
            var content = await Api.GetCharacterAsync(nombreUser);
           string nombre2= content.Nombre;
            

            if (content == null)
            {
                Console.WriteLine("Personaje no encontrado.");
                return;
            }

        
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
    }
}
