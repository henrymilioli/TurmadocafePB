namespace Cafeteria.DTO
{
    public class AvaliacaoDTO
    {
        public int Nota { get; set; }

        public string Comentario { get; set; }

        public Guid CafeteriaId { get; set; }

        public Guid UsuarioId { get; set; }

    }
}
