using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RickApp;


namespace RickApp
{
    class Program
    {
        static async Task Main()
        {

           await Function.EjecutarMenu();
        }
    }
}