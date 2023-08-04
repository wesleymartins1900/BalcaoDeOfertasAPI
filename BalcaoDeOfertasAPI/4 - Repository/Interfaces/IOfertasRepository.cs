using DtosBalcaoDeOfertas.InputDTO;
using BalcaoDeOfertasAPI._1___Models;

namespace BalcaoDeOfertasAPI._4___Repository.Interfaces
{
    public interface IOfertasRepository
    {
        Task<IList<Oferta>> GetBalcaoDeOfertasByPage(int page, int pageSize);

        Task<IList<Oferta>> GetBalcaoDeOfertasByScroll(int scrollId, int pageSize);

        Task<long> CriarOferta(Oferta oferta);

        Task<Oferta?> LocalizarOfertaById(long id);

        Task AtualizarOferta(Oferta oferta);

        Task<int> QuantidadeOfertasPorDiaPorUsuario(Guid usuarioId);

        Task<bool> ExisteSaldoParaCriacaoDaOferta(NovaOfertaInputDTO inputDto);
    }
}