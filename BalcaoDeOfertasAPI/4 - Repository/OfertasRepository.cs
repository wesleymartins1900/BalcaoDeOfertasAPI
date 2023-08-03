using Microsoft.EntityFrameworkCore;
using BalcaoDeOfertasAPI._1___Models;
using BalcaoDeOfertasAPI._4___Repository.Context;
using BalcaoDeOfertasAPI._4___Repository.Interfaces;

namespace BalcaoDeOfertasAPI._4___Repository
{
    public class OfertasRepository : IOfertasRepository
    {
        private readonly DbApiContext _context;

        public OfertasRepository(DbApiContext context)
        {
            _context = context;
        }

        public async Task CriarOferta(Oferta oferta)
        {
            await _context.Ofertas.AddAsync(oferta);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarOferta(Oferta oferta)
        {
            _context.Update(oferta);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Oferta>> GetBalcaoDeOfertasByPage(int page, int pageSize) => await _context.Ofertas.Skip(page)
                                                                                                                   .Take(pageSize)
                                                                                                                   .OrderByDescending(x => x.DataEHoraInclusao)
                                                                                                                   .ToListAsync();

        public async Task<IList<Oferta>> GetBalcaoDeOfertasByScroll(int scrollId, int pageSize) => await _context.Ofertas.Where(x => x.Id > scrollId)
                                                                                                                         .Take(pageSize)
                                                                                                                         .OrderByDescending(x => x.DataEHoraInclusao)
                                                                                                                         .ToListAsync();

        public async Task<Oferta?> LocalizarOfertaById(long id) => await _context.Ofertas.FindAsync(id);

        public async Task<int> QuantidadeOfertasPorDiaPorUsuario(Guid usuarioId) => await _context.Ofertas.Where(x => x.UsuarioId == usuarioId).CountAsync();
    }
}