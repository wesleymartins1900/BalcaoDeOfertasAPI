using BalcaoDeOfertasAPI._1___Models;

namespace BalcaoDeOfertasAPI._4___Repository.Interfaces
{
    public interface IMoedasRepository
    {
        Task<Moeda?> CarregaDadosDaCarteiraDoUsuarioAsync(Guid moedaId, Guid usuarioId);
    }
}