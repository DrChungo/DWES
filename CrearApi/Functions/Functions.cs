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

        //Con esto hacemos que no se repitan las canciones
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

            // Canción aleatoria del JSON
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

        // ============ PREGUNTAS (solo I/O + llamada a validaciones) ============

        private static int AskSubgenres(Track track)
        {
            int subgenreCount = track.Subgenres.Count;

            Console.WriteLine($"\nLa canción tiene {subgenreCount} subgénero(s).");
            Console.WriteLine($"Escribe exactamente {subgenreCount}, separados por coma:");

            string? userInput = Console.ReadLine();

            var userSubgenres = userInput?
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(s => s.ToLower())
                .ToList() ?? new List<string>();

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
            Console.WriteLine("\n=== RESULTADO ===");
            Console.WriteLine($"Puntuación total: {score}/10");

            string rank = GameValidations.GetRank(score);
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
