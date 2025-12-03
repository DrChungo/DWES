namespace TechnoApi.Models
{
    // ------------------------------
    //  MODELO PRINCIPAL DEL TRACK
    // ------------------------------
    public class Track
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public Artist Artist { get; set; }

        public string Url { get; set; } = "";

        public int Bpm { get; set; }

        public List<string> Subgenres { get; set; }

        public ReleaseInfo ReleaseInfo { get; set; }

        public List<string> Tags { get; set; }

        public MixInfo MixInfo { get; set; }

        public int MainDrop { get; set; }

        public string Key { get; set; } = "";
    }

    // ------------------------------
    //  ARTISTA
    // ------------------------------
    public class Artist
    {
        public string Name { get; set; } = "";
        public string Country { get; set; } = "";
        public int ActiveSince { get; set; }
    }

    // ------------------------------
    //  INFORMACIÓN DE LANZAMIENTO
    // ------------------------------
    public class ReleaseInfo
    {
        public string Label { get; set; }
        public int Year { get; set; }
    }

    // ------------------------------
    //  INFORMACIÓN DEL MIX
    // ------------------------------
    public class MixInfo
    {
        public string Type { get; set; }
        public string Remixer { get; set; }
    }

    // ------------------------------
    //  MODELO PARA EL JUEGO
    // ------------------------------
    public class PlayerGuess
    {
        public int Bpm { get; set; }
        public string Key { get; set; } = "";
        public int MainDrop { get; set; }
        public string Subgenre { get; set; } = "";
    }

    public class GameResult
    {
        public string Message { get; set; } = "";
        public string Rank { get; set; } = "";

        public GameResult(string message, string rank)
        {
            Message = message;
            Rank = rank;
        }
    }
}
