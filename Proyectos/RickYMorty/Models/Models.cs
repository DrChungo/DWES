using System.Text.Json.Serialization;

namespace RickYmortyApi.Models
{
    // Modelo que representa EXACTAMENTE el JSON de la API externa.
    // Cambia los campos según lo que devuelva tu API.
    public class ApiResponse
    {
        [JsonPropertyName("results")]
        public List<Character> Results {get;set;}=new();
    }

    public class Character
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("status")]
        public string Status { get; set; } = "";

        [JsonPropertyName("species")]
        public string Species { get; set; } = "";

        [JsonPropertyName("gender")]
        public string Gender { get; set; } = "";



    }

    // Modelo que TU PROGRAMA usará para guardar datos.
    // Puedes cambiar los nombres y propiedades.
    public class SavedItem
    {
        public string Name { get; set; } = "";
        public string Status { get; set; } = "";
        public string Species { get; set; } = "";
        public string Gender { get; set; } = "";
    }
}
