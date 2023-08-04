namespace BalcaoDeOfertasAPI._1___Models
{
    public class Carteira
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}