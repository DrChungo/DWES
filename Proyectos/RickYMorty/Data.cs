using System.Collections.Generic;

namespace RickApp
{
    public static class Data
    {
        public static List<Personaje> PersonajesGuardados { get; } = new();
    }

    public class Personaje
    {
        public string Nombre { get; set; } = "";
        public string Estado { get; set; } = "";
        public string Especie { get; set; } = "";
        public string Genero { get; set; } = "";

        public override string ToString()
        {
            return $"{Nombre} ({Especie} - {Estado})";
        }
    }
}
