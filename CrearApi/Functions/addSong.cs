using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrearApi.Models;
using CrearApi.Services;

namespace CrearApi.Functions
{
    public static class FunctionsAdd
    {
        public static async Task AddSongAsync()
        {
            Console.Clear();
            Console.WriteLine("=== AÑADIR CANCIÓN ===\n");

            var api = new ApiService();
            var tracks = await api.GetAllTracksAsync();

            int newId = 1;
            if (tracks != null && tracks.Count > 0)
                newId = tracks.Max(t => t.Id) + 1;

            Console.WriteLine($"ID asignado automáticamente: {newId}\n");

            // ======== Datos básicos ========
            Console.Write("Título: ");
            string title = ReadNonEmpty();

            Console.Write("URL (YouTube, etc.): ");
            string url = ReadNonEmpty();

            Console.Write("BPM (número entero): ");
            int bpm = ReadInt();

            Console.Write("Main drop (segundos, entero): ");
            int mainDrop = ReadInt();

            Console.Write("Tonalidad (ej: 'E minor'): ");
            string key = ReadNonEmpty();

            // ======== Artista ========
            Console.WriteLine("\n--- ARTISTA ---");
            Console.Write("Nombre del artista: ");
            string artistName = ReadNonEmpty();

            Console.Write("País del artista (ej: 'Spain'): ");
            string artistCountry = ReadNonEmpty();

            Console.Write("Año de inicio de actividad (ej: 2020): ");
            int activeSince = ReadInt();

            // ======== ReleaseInfo ========
            Console.WriteLine("\n--- RELEASE INFO ---");
            Console.Write("Sello (label): ");
            string label = ReadNonEmpty();

            Console.Write("Año de lanzamiento: ");
            int year = ReadInt();

            // ======== MixInfo ========
            Console.WriteLine("\n--- MIX INFO ---");
            Console.Write("Tipo de mix (original, remix, bootleg...): ");
            string mixType = ReadNonEmpty();

            string? remixer = null;
            if (!mixType.Equals("original", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Nombre del remixer (o deja vacío si no aplica): ");
                string remixerInput = Console.ReadLine()?.Trim() ?? "";
                remixer = string.IsNullOrWhiteSpace(remixerInput) ? null : remixerInput;
            }

            // ======== Listas: subgéneros y tags ========
            Console.WriteLine("\nSubgéneros (separados por comas, ej: hard techno, emotional rave): ");
            var subgenres = ReadStringList();

            Console.WriteLine("Tags (separados por comas, ej: emotional, vocal, rave): ");
            var tags = ReadStringList();

            // Construimos el objeto Track
            var newTrack = new Track
            {
                Id = newId,
                Title = title,
                Url = url,
                Bpm = bpm,
                MainDrop = mainDrop,
                Key = key,
                Subgenres = subgenres,
                Tags = tags,
                Artist = new Artist
                {
                    Name = artistName,
                    Country = artistCountry,
                    ActiveSince = activeSince
                },
                ReleaseInfo = new ReleaseInfo
                {
                    Label = label,
                    Year = year
                },
                MixInfo = new MixInfo
                {
                    Type = mixType,
                    Remixer = remixer
                }
            };

            Console.WriteLine("\nEnviando canción a la API...");
            bool ok = await api.AddTrackAsync(newTrack);

            if (ok)
            {
                Console.WriteLine("\nCanción añadida correctamente al JSON de la API. ");
                Console.WriteLine($"ID: {newTrack.Id} | Título: {newTrack.Title}");
            }
            else
            {
                Console.WriteLine("\nError al añadir la canción en la API.");
            }
        }


        private static string ReadNonEmpty()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.Write("Valor obligatorio. Intenta de nuevo: ");
            }
        }

        private static int ReadInt()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int value))
                    return value;

                Console.Write("Número no válido. Intenta de nuevo: ");
            }
        }

        private static List<string> ReadStringList()
        {
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return new List<string>();

            return input
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();
        }
    }
}
