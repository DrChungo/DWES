using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CrearApi.Models;
using CrearApi.Services;

namespace CrearApi.Functions
{
    public static class GameFunctions
    {
        private static readonly Random _rng = new Random();

        public static async Task RunAsync()
        {
            var api = new ApiService();
            var tracks = await api.GetAllTracksAsync();

            if (tracks == null || tracks.Count == 0)
            {
                Console.WriteLine("No se han podido obtener tracks de la API.");
                return;
            }

            // Canción aleatoria del JSON, no se repite
            var track = tracks[_rng.Next(tracks.Count)];

            Console.Clear();
            Console.WriteLine("=== TECHNO EAR-TRAINING GAME ===\n");
            Console.WriteLine($"Título:  {track.Title}");
            Console.WriteLine($"Artista: {track.Artist.Name}");
            Console.WriteLine($"Escucha la canción aquí: {track.Url}\n");

            Console.WriteLine("Pulsa ENTER cuando hayas escuchado un poco...");
            Console.ReadLine();

            int score = 0;

            score += AskSubgenres(track);
            score += AskBpm(track);
            score += AskMainDrop(track);
            score += AskKey(track);

            ShowResult(track, score);
        }

        

        private static int AskSubgenres(Track track)
        {
            int subgenreCount = track.Subgenres.Count;

            Console.WriteLine($"\nLa canción tiene {subgenreCount} subgénero(s).");

            List<string> userSubgenres = new List<string>();

            for (int i = 0; i < subgenreCount; i++)
            {
                Console.Write($"Género número {i + 1}: ");
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (!string.IsNullOrWhiteSpace(input))
                    userSubgenres.Add(input);
                else
                    userSubgenres.Add(string.Empty);
            }

            return GameValidations.CalculateSubgenreScore(track, userSubgenres);
        }


        private static int AskBpm(Track track)
        {
            Console.WriteLine("\n¿A cuántos BPM crees que va la canción?");

            bool parsed = int.TryParse(Console.ReadLine(), out int userBpm);
            if (!parsed)
            {
                Console.WriteLine("Entrada no válida, se toma como 0 BPM.");
                userBpm = 0;
            }

            return GameValidations.CalculateBpmScore(track, userBpm);
        }

        private static int AskMainDrop(Track track)
        {
            Console.WriteLine("\n¿En qué segundo dirías que entra el main drop?");

            bool parsed = int.TryParse(Console.ReadLine(), out int userDrop);
            if (!parsed)
            {
                Console.WriteLine("Entrada no válida, se toma como 0 segundos.");
                userDrop = 0;
            }

            return GameValidations.CalculateMainDropScore(track, userDrop);
        }

        private static int AskKey(Track track)
        {
            Console.WriteLine("\n¿En qué tonalidad crees que está? (ej: 'F minor', 'G# minor')");
            string? userKey = Console.ReadLine();

            return GameValidations.CalculateKeyScore(track, userKey);
        }

        // ================= RESULTADO =================

      private static void ShowResult(Track track, int score)
{
   
    int percentage = (int)Math.Round((score * 100.0) / GameValidations.MAX_SCORE);

    Console.WriteLine("\n=== RESULTADO ===");
    Console.WriteLine($"Puntuación total: {percentage}/100");

    string rank = GameValidations.GetRankPercentage(percentage);
    Console.WriteLine($"Rango: {rank}");

    Console.WriteLine($"\nDatos reales del tema:");
    Console.WriteLine($"Subgéneros: {string.Join(", ", track.Subgenres)}");
    Console.WriteLine($"BPM:        {track.Bpm}");
    Console.WriteLine($"Main Drop:  {track.MainDrop} s");
    Console.WriteLine($"Tonalidad:  {track.Key}");

    Console.WriteLine("\nPulsa ENTER para salir...");
    Console.ReadLine();
}

    }
}
