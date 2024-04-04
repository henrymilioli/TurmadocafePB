using System.Text.Json.Serialization;

namespace Cafeteria.Models
{
    public class Avaliacao
    {
        public Guid Id { get; set; }

        public int Nota { get; set; }

        public string Comentario { get; set; }


        [JsonIgnore]
        public CafeteriaC  Cafeterias { get; set; }

        [JsonIgnore]
        public Usuario Usuarios { get; set; }
    }
}
