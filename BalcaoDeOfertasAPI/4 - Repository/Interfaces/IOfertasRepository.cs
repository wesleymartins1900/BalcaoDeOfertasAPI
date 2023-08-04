using BalcaoDeOfertasAPI._1___Models;

namespace BalcaoDeOfertasAPI._4___Repository.Interfaces
{
    public interface IOfertasRepository
    {
        Task<IList<Oferta>> GetBalcaoDeOfertasByPageAsync(int page, int pageSize);

        Task<IList<Oferta>> GetBalcaoDeOfertasByScrollAsync(int scrollId, int pageSize);

        Task<long> CriarOfertaAsync(Oferta oferta);

        Task<Oferta?> LocalizarOfertaByIdAsync(long id);

        Task AtualizarOfertaAsync(Oferta oferta);

        Task<int> QuantidadeOfertasPorDiaPorUsuarioAsync(Guid usuarioId);

        Task<int> SomaDaQuantidadeTotalDaMoedaEmOfertasAsync(Guid moedaId);
    }
}