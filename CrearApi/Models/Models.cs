using System.Collections.Generic;

namespace CrearApi.Models
{

    public class Track
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";
        public Artist Artist { get; set; } = new();

        public string Url { get; set; } = "";

        public int Bpm { get; set; }
        public List<string> Subgenres { get; set; } = new();
        public ReleaseInfo ReleaseInfo { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public MixInfo MixInfo { get; set; } = new();
        public int MainDrop { get; set; }

        public string Key { get; set; } = "";
    }


    public class Artist
    {
        public string Name { get; set; } = "";
        public string Country { get; set; } = "";
        public int ActiveSince { get; set; }
    }


    public class ReleaseInfo
    {
        public string Label { get; set; } = "";
        public int Year { get; set; }
    }


    public class MixInfo
    {
        public string Type { get; set; } = "";
        public string? Remixer { get; set; }
    }
}
