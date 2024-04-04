namespace Cafeteria.DTO
{
    public class EventoDTO
    {

        public string Nome { get; set; }

        public DateTime Data { get; set; }

        public Guid CafeteriaId { get; set; }

        public Guid UsuarioId { get; set; }

    }
}
