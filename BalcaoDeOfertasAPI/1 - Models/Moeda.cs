﻿namespace BalcaoDeOfertasAPI._1___Models
{
    public class Moeda
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int QuantidadeTotal { get; set; }
        public double ValorReal { get; set; }
        public Guid CarteiraId { get; set; }
    }
}