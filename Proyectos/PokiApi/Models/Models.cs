using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace PokeApi
{
    // Datos que vienen de la API

    
    public class Pokemon
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; } 

        [JsonPropertyName("types")]
        public List<PokemonTypeSlot> Types { get; set; } = new();

    }

    //No llamar  a las clases como a los objetos (menos lio)
    public class PokemonTypeSlot
    {
        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        [JsonPropertyName("type")]
        public PokemonTypeInfo Type { get; set; } = new();
      
    }
    public class PokemonTypeInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

    }



    // Datos que tú guardas en tu lista
    public class SavedItem
    {
        public string Name { get; set; } = "";
        public int Id { get; set; } 
        public string Type { get; set; } = "";
        public int Weight { get; set; } 
        public int Height { get; set; } 
    }
}
