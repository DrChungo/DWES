using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CrearApi.Models;
using CrearApi.Services;

namespace CrearApi.Functions
{
    /**
     * Controla la ejecución del juego de entrenamiento auditivo.
     * Obtiene canciones desde la API, elige una aleatoria y calcula la puntuación final.
     */
    public static class GameFunctions
    {
        private static readonly Random _rng = new Random();

        /**
         * Ejecuta una partida completa del juego:
         * - Obtiene canciones desde la API
         * - Selecciona una canción aleatoria
         * - Pregunta al usuario subgéneros, BPM, main drop y tonalidad
         * - Muestra el resultado final
         *
         * @return Task que representa la ejecución asíncrona de la partida
         */
        public static async Task RunAsync()
        {
            var api = new ApiService();
            var tracks = await api.GetAllTracksAsync();

            // Si la API no devuelve datos, se cancela el juego
            if (tracks == null || tracks.Count == 0)
            {
                Console.WriteLine("No se han podido obtener tracks de la API.");
                return;
            }

            // Selección aleatoria de una canción
            var track = tracks[_rng.Next(tracks.Count)];

            Console.Clear();
            Console.WriteLine("=== TECHNO EAR-TRAINING GAME ===\n");
            Console.WriteLine($"Título:  {track.Title}");
            Console.WriteLine($"Artista: {track.Artist.Name}");
            Console.WriteLine($"Escucha la canción aquí: {track.Url}\n");

            Console.WriteLine("Pulsa ENTER cuando hayas escuchado un poco...");
            Console.ReadLine();

            // Acumulador de puntuación
            int score = 0;
            score += AskSubgenres(track);
            score += AskBpm(track);
            score += AskMainDrop(track);
            score += AskKey(track);

            ShowResult(track, score);
        }

        /**
         * Pide al usuario los subgéneros del tema y calcula la puntuación obtenida
         * comparándolos con los subgéneros reales.
         *
         * @param track Canción seleccionada con sus subgéneros reales
         * @return Puntuación obtenida en subgéneros (0 a 2)
         */
        private static int AskSubgenres(Track track)
        {
            int subgenreCount = track.Subgenres.Count;
            Console.WriteLine($"\nLa canción tiene {subgenreCount} subgénero(s).");

            List<string> userSubgenres = new List<string>();

            for (int i = 0; i < subgenreCount; i++)
            {
                Console.Write($"Género número {i + 1}: ");
                string? input = Console.ReadLine()?.Trim().ToLower();

                // Si el usuario deja vacío, se guarda como string vacío
                userSubgenres.Add(string.IsNullOrWhiteSpace(input) ? string.Empty : input);
            }

            return GameValidations.CalculateSubgenreScore(track, userSubgenres);
        }

        /**
         * Pide al usuario el BPM estimado del tema y calcula la puntuación comparando
         * con el BPM real.
         *
         * @param track Canción seleccionada con el BPM real
         * @return Puntuación obtenida en BPM (0 a 3)
         */
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

        /**
         * Pide al usuario el segundo aproximado en el que entra el main drop
         * y calcula la puntuación comparando con el valor real.
         *
         * @param track Canción seleccionada con el main drop real
         * @return Puntuación obtenida en main drop (0 a 2)
         */
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

        /**
         * Pide al usuario la tonalidad del tema y calcula la puntuación comparando con la tonalidad real.
         *
         * @param track Canción seleccionada con la tonalidad real
         * @return Puntuación obtenida en tonalidad (0 o 1)
         */
        private static int AskKey(Track track)
        {
            Console.WriteLine("\n¿En qué tonalidad crees que está? (ej: 'F minor', 'G# minor')");
            string? userKey = Console.ReadLine();

            return GameValidations.CalculateKeyScore(track, userKey);
        }

        /**
         * Muestra por consola el resultado final de la partida: porcentaje total, rango obtenido, datos reales del tema
         *
         * @param track Canción seleccionada con los datos reales
         * @param score Puntuación total obtenida (0 a MAX_SCORE)
         */
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
