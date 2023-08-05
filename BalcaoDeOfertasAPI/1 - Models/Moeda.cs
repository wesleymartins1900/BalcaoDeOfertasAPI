namespace BalcaoDeOfertasAPI._1___Models
{
    public class Moeda
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int QuantidadeTotal { get; set; }
        public decimal ValorReal { get; set; }

        public Guid CarteiraId { get; set; }
        public Carteira Carteira { get; set; }
    }
}