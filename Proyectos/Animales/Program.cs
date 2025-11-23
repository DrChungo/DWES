using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AnimalsApi;
using AnimalsApi.Models;
using AnimalsApi.Services;
using AnimalsApi.Funcions;


namespace AnimalsApi
{
    class Program
    {

        static async Task Main(string[] arg)
        {

            var service = new DogApiService();
            var saveBreed = new List<DogBred>();

            string opcion;



            do
            {


                Console.WriteLine("=== MINI GESTOR DE PERROS ===");
                Console.WriteLine("1. Buscar raza de perro");
                Console.WriteLine("2. Listar razas guardadas");
                Console.WriteLine("0. Salir");
                Console.Write("Elige una opción: ");
                opcion = Console.ReadLine();



                switch (opcion)
                {


                    case "1":

                        //Usamos await solo si la funcion es async
                        await Functions.SeachBreed(service, saveBreed);
                        break;
                    case "2":
                        Functions.listBreed(saveBreed);
                        break;

                    case "0":
                        System.Console.WriteLine("Saliendo...");
                        break;
                    default:
                        System.Console.WriteLine("Opcion incorrecta");
                        break;
                }


            } while (opcion != "0");





        }



    }
}