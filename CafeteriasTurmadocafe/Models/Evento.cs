using System.Text.Json.Serialization;

namespace Cafeteria.Models
{
    public class Evento
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public DateTime Data { get; set; }


        [JsonIgnore]
        public CafeteriaC  Cafeterias { get; set; }

        [JsonIgnore]
        public Usuario Usuarios { get; set; }
    }
}
