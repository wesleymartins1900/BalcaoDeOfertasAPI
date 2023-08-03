using DtosBalcaoDeOfertas.InputDTO;
using BalcaoDeOfertasAPI._1___Models;

namespace BalcaoDeOfertasAPI._3___Services.Interfaces
{
    public interface IOfertasService
    {
        Task<IList<Oferta>> GetBalcaoDeOfertas(int page, int pageSize, string? scrollId);

        Task<OfertaInputDTO> CriarOferta(OfertaInputDTO inputDto);

        Task<OfertaInputDTO> ExcluirOferta(OfertaInputDTO inputDto);
    }
}