using BalcaoDeOfertasAPI._1___Models;

namespace BalcaoDeOfertasAPI._3___Services.Interfaces
{
    public interface IMoedasService
    {
        Task<Moeda?> CarregarDadosDaCarteiraDoUsuarioAsync(Guid moedaId, Guid usuarioId);
    }
}