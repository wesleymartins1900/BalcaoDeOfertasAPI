using Microsoft.EntityFrameworkCore;
using BalcaoDeOfertasAPI._1___Models;
using BalcaoDeOfertasAPI._4___Repository.Context;
using BalcaoDeOfertasAPI._4___Repository.Interfaces;

namespace BalcaoDeOfertasAPI._4___Repository
{
    public class MoedasRepository : IMoedasRepository
    {
        private readonly DbApiContext _context;

        public MoedasRepository(DbApiContext context)
        {
            _context = context;
        }

        public async Task<Moeda?> CarregaDadosDaCarteiraDoUsuarioAsync(Guid moedaId, Guid usuarioId) => await _context.Moedas
                                                                                                                      .Include(c => c.Carteira)
                                                                                                                      .Where(x => x.Id == moedaId
                                                                                                                               && x.Carteira.UsuarioId == usuarioId)
                                                                                                                      .FirstOrDefaultAsync();
    }
}