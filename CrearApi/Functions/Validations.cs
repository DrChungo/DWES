using System;
using System.Collections.Generic;
using System.Linq;
using CrearApi.Models;

namespace CrearApi.Functions
{
    public static class GameValidations
    {
      
        public const int MAX_SCORE = 8;

        // SUBGÉNEROS 
        public static int CalculateSubgenreScore(Track track, List<string> userSubgenres)
        {
            var realSubgenres = track.Subgenres
                .Select(s => s.ToLower())
                .ToList();

            int correctMatches = userSubgenres
                .Count(s => realSubgenres.Contains(s));

            // Todos correctos
            if (correctMatches == realSubgenres.Count && realSubgenres.Count > 0)
                return 2;

            // Alguno correcto 
            if (correctMatches > 0)
                return 1;

            // Ninguno "YAGO"
            return 0;
        }

        // BPM (0–3 puntos)
        public static int CalculateBpmScore(Track track, int userBpm)
        
        {
            int diff = Math.Abs(userBpm - track.Bpm);

            if (diff == 0)
                return 3; 
            if (diff <= 2)
                return 2; 
            if (diff <= 5)
                return 1; 

            return 0;
        }

        // MAIN DROP (0–2 puntos)
        public static int CalculateMainDropScore(Track track, int userDrop)
        {
            int diff = Math.Abs(userDrop - track.MainDrop);

            if (diff <= 3)
                return 2;
            if (diff <= 7)
                return 1;

            return 0;
        }

        // TONALIDAD (0–1 punto)
        public static int CalculateKeyScore(Track track, string? userKey)
        {
            if (!string.IsNullOrWhiteSpace(userKey) &&
                string.Equals(userKey.Trim(), track.Key, StringComparison.OrdinalIgnoreCase))
            {
                return 1;
            }

            return 0;
        }

        // RANGO FINAL USANDO PORCENTAJE 
        public static string GetRankPercentage(int percentage) => percentage switch
        {
            >= 90 => "S",
            >= 75 => "A",
            >= 50 => "B",
            >= 25 => "C",
            _ => "YAGO",
        };
    }
}
