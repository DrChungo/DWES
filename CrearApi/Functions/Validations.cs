using System;
using System.Collections.Generic;
using System.Linq;
using CrearApi.Models;
using CrearApi;

namespace CrearApi.Functions
{
    /**
     * Clase que contiene las validaciones y cálculos de puntuación para el juego.
     */
    public static class GameValidations
    {
     
        public const int MAX_SCORE = 8;

        /**
         * Calcula la puntuación en base a los subgéneros introducidos
         * por el usuario comparándolos con los subgéneros reales del tema.
         *
         * @param track Canción con los subgéneros reales
         * @param userSubgenres Lista de subgéneros introducidos por el usuario
         * @return Puntuación obtenida (0 a 2)
         */
        public static int CalculateSubgenreScore(Track track, List<string> userSubgenres)
        {
            var realSubgenres = track.Subgenres
                .Select(s => s.ToLower())
                .ToList();

            int correctMatches = userSubgenres
                .Count(s => realSubgenres.Contains(s));

     
            if (correctMatches == realSubgenres.Count && realSubgenres.Count > 0)
                return 2;

            if (correctMatches > 0)
                return 1;

         
            return 0;
        }

        /**
         * Calcula la puntuación del BPM comparando el valor introducido
         * por el usuario con el BPM real de la canción.
         *
         * @param track Canción con el BPM real
         * @param userBpm BPM introducido por el usuario
         * @return Puntuación obtenida (0 a 3)
         */
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

        /**
         * Calcula la puntuación del momento del main drop comparando
         * el segundo indicado por el usuario con el real.
         *
         * @param track Canción con el main drop real
         * @param userDrop Segundo indicado por el usuario
         * @return Puntuación obtenida (0 a 2)
         */
        public static int CalculateMainDropScore(Track track, int userDrop)
        {
            int diff = Math.Abs(userDrop - track.MainDrop);

            if (diff <= 3)
                return 2;
            if (diff <= 7)
                return 1;

            return 0;
        }

        /**
         * Calcula la puntuación de la tonalidad comparando la introducida
         * por el usuario con la tonalidad real del tema.
         *
         * @param track Canción con la tonalidad real
         * @param userKey Tonalidad introducida por el usuario
         * @return Puntuación obtenida (0 o 1)
         */
        public static int CalculateKeyScore(Track track, string? userKey)
        {
            if (!string.IsNullOrWhiteSpace(userKey) &&
                string.Equals(userKey.Trim(), track.Key, StringComparison.OrdinalIgnoreCase))
            {
                return 1;
            }

            return 0;
        }

        /**
         * Devuelve el rango final del jugador en función del porcentaje total obtenido.
         *
         * @param percentage Porcentaje final de puntuación
         * @return Letra correspondiente al rango (S, A, B, C, YAGO)
         */
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
