namespace BalcaoDeOfertasAPI._1___Models
{
    public class Oferta
    {
        public long Id { get; set; }
        public double PrecoUnitario { get; set; }
        public double Quantidade { get; set; }
        public string Motivo { get; set; }
        public DateTime DataEHoraInclusao { get; set; }
        public Guid UsuarioId { get; set; }
        public bool Excluido { get; set; }
    }
}