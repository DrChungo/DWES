using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TechnoApi.Services;
using TechnoApi.Functions;
using CrearApi.Models;

namespace TechnoApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service   = new ApiService();
            var savedList = new List<Track>();

            string opcion;
            Functions.Functions.ListItems(savedList);

        }
    }
}
