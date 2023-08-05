namespace BalcaoDeOfertasAPI._1___Models
{
    public class Oferta
    {
        public long Id { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataEHoraInclusao { get; set; }
        public DateTime DataEHoraExclusao { get; set; }
        public bool Excluido { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public Guid MoedaId { get; set; }
        public Moeda Moeda { get; set; }
    }
}