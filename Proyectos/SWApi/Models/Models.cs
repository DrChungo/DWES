using System.Text.Json.Serialization;

namespace SWApi
{
    // JSON raíz: por ejemplo { "results": [ {...}, {...} ] }


    //Llamar directramete en el que queremos empezar

    //Cuando el objeto del Json tiene [ ] usamos list

    //Si solo tiene { } usamos un ojeto Ejemplo en poke api Info
    public class ApiResponse
    {
        [JsonPropertyName("results")]
        public List<Starships> Results { get; set; } = new();
    }

    // Datos que vienen de la API
    public class Starships
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("model")]
        public string Model { get; set; } = "";

        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; } = "";

        [JsonPropertyName("max_atmosphering_speed")]
        public string Speed { get; set; } = "";

        [JsonPropertyName("crew")]
        public string Crew { get; set; } = "";
    }

    // Datos que tú guardas en tu lista
    public class SavedItem
    {
        public string Name { get; set; } = "";
        public string Model { get; set; } = "";
        public string Manufacturer { get; set; } = "";
        public string Speed { get; set; } = "";
        public string Crew { get; set; } = "";
    }
}
