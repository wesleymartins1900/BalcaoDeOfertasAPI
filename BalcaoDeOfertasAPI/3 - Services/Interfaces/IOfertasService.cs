using DtosBalcaoDeOfertas.InputDTO;
using BalcaoDeOfertasAPI._1___Models;

namespace BalcaoDeOfertasAPI._3___Services.Interfaces
{
    public interface IOfertasService
    {
        Task<IList<Oferta>> GetBalcaoDeOfertasAsync(int page, int pageSize, string? scrollId);

        Task<OfertaOutputDTO> CriarOfertaAsync(NovaOfertaInputDTO inputDto);

        Task<OfertaOutputDTO> ExcluirOfertaAsync(ExcluirOfertaInputDTO inputDto);
    }
}