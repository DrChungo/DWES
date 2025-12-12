using System;
using System.Threading.Tasks;
using CrearApi.Functions;

namespace CrearApi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            NodeLauncher.StartNodeApi();
            Console.WriteLine("Iniciando API Node...\n");

            // 👇 Darle un poco de aire a Node para arrancar
            await Task.Delay(1000);

            string? option;

            do
            {
                await GameFunctions.RunAsync();

                Console.WriteLine("\nPulsa 0 para finalizar.");
                Console.WriteLine("Pulsa cualquier otra tecla para continuar...\n");

                option = Console.ReadLine()?.Trim();
            }
            while (option != "0");

            NodeLauncher.StopNodeApi();
            Console.WriteLine("API Node detenida.");
            Console.WriteLine("Programa finalizado.");
        }
    }
}
