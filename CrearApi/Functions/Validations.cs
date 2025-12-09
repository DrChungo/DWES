using System;
using System.Collections.Generic;
using System.Linq;
using CrearApi.Models;

namespace CrearApi.Functions
{
    public static class GameValidations
    {
        // Subgéneros: máx 2 puntos
        public static int CalculateSubgenreScore(Track track, List<string> userSubgenres)
        {
            int score = 0;

            var realSubgenres = track.Subgenres
                .Select(s => s.ToLower())
                .ToList();

            int correctMatches = userSubgenres.Count(s => realSubgenres.Contains(s));

            if (correctMatches == realSubgenres.Count && realSubgenres.Count > 0)
                score += 2;
            else if (correctMatches > 0)
                score += 1;

            return score;
        }

        // BPM: máx 3 puntos
        public static int CalculateBpmScore(Track track, int userBpm)
        {
            int score = 0;

            int diff = Math.Abs(userBpm - track.Bpm);

            if (diff == 0) score += 3;
            else if (diff <= 2) score += 2;
            else if (diff <= 5) score += 1;

            return score;
        }

        // Main Drop: máx 2 puntos
        public static int CalculateMainDropScore(Track track, int userDrop)
        {
            int score = 0;

            int diff = Math.Abs(userDrop - track.MainDrop);

            if (diff <= 3) score += 2;
            else if (diff <= 7) score += 1;

            return score;
        }

        // Tonalidad: máx 1 punto
        public static int CalculateKeyScore(Track track, string? userKey)
        {
            int score = 0;

            if (!string.IsNullOrWhiteSpace(userKey) &&
                string.Equals(userKey.Trim(), track.Key, StringComparison.OrdinalIgnoreCase))
            {
                score += 1;
            }

            return score;
        }

        // Rango final según la puntuación total
        public static string GetRank(int score) => score switch
        {
            >= 9 => "S",
            >= 7 => "A",
            >= 5 => "B",
            >= 3 => "C",
            _ => "YAGO"
        };
    }
}
