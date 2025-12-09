using System.Threading.Tasks;
using CrearApi.Functions;

namespace CrearApi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await GameFunctions.RunAsync();
        }
    }
}
