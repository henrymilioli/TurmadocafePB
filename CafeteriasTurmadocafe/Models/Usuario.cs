using System.Text.Json.Serialization;

namespace Cafeteria.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public List<CafeteriaC> Cafeterias { get; set; }

        [JsonIgnore]
        public List<Avaliacao> Avaliacoes { get; set; }


        [JsonIgnore]
        public List<Evento> Eventos { get; set; }


    }
}
