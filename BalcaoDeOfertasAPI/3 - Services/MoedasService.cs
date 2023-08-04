using BalcaoDeOfertasAPI._1___Models;
using BalcaoDeOfertasAPI._3___Services.Interfaces;
using BalcaoDeOfertasAPI._4___Repository.Interfaces;

namespace BalcaoDeOfertasAPI._3___Services
{
    public class MoedasService : IMoedasService
    {
        private readonly IMoedasRepository _moedasRepository;

        public MoedasService(IMoedasRepository moedasRepository)
        {
            _moedasRepository = moedasRepository;
        }

        public async Task<Moeda?> CarregarDadosDaCarteiraDoUsuarioAsync(Guid moedaId, Guid usuarioId) => await _moedasRepository.CarregaDadosDaCarteiraDoUsuarioAsync(moedaId, usuarioId);
    }
}