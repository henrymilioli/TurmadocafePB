using System.Text.Json.Serialization;

namespace Cafeteria.Models
{
    public class CafeteriaC
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Endereco { get; set; }


        // Relacionamento 1
        // UM USUARIO VAI TER N CAFETERIAS 
        // 1 -> N
        [JsonIgnore]
        public Usuario usuario { get; set; }


        [JsonIgnore]
        public List<Avaliacao> Avaliacoes { get; set; }

        [JsonIgnore]
        public List<Evento> Eventos { get; set; }
       }
}
