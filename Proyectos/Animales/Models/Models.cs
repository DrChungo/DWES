using System.Text.Json.Serialization;

namespace AnimalsApi.Models
{
    // ----------------------------------------------------------
    // 🔁 ***CAMBIAR ESTA CLASE SEGÚN TU API***
    // Ajusta los nombres de las propiedades para que coincidan
    // con el JSON REAL de tu API.
    // ----------------------------------------------------------

    //Aqui usamos para buscar en la api
    public class DogApiResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }="";

      [JsonPropertyName("status")]
        public string Status { get; set; }="";
      

    }
//Aqui lo guardamos en el objeto para mostrarlo luego
    public class DogBred
    {
        public string Name {get; set;}="";
         public string ImageUrl {get; set;}="";
    }
}
