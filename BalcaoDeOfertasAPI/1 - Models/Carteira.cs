namespace BalcaoDeOfertasAPI._1___Models
{
    public class Carteira
    {
        public Guid Id { get; set; }
        public List<Moeda> Moedas { get; set; }
        public Usuario Usuario { get; set; }
    }
}