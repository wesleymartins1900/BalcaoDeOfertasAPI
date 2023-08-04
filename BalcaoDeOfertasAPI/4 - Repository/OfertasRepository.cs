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

        public async Task<long> CriarOfertaAsync(Oferta oferta)
        {
            var result = await _context.Ofertas.AddAsync(oferta);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task AtualizarOfertaAsync(Oferta oferta)
        {
            _context.Update(oferta);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Oferta>> GetBalcaoDeOfertasByPageAsync(int page, int pageSize) => await _context.Ofertas.Where(x => !x.Excluido)
                                                                                                                        .Skip(page)
                                                                                                                        .Take(pageSize)
                                                                                                                        .OrderByDescending(x => x.DataEHoraInclusao)
                                                                                                                        .ToListAsync();

        public async Task<IList<Oferta>> GetBalcaoDeOfertasByScrollAsync(int scrollId, int pageSize) => await _context.Ofertas.Where(x => x.Id > scrollId
                                                                                                                                  && !x.Excluido)
                                                                                                                              .Take(pageSize)
                                                                                                                              .OrderByDescending(x => x.DataEHoraInclusao)
                                                                                                                              .ToListAsync();

        public async Task<Oferta?> LocalizarOfertaByIdAsync(long id) => await _context.Ofertas.FindAsync(id);

        public async Task<int> QuantidadeOfertasPorDiaPorUsuarioAsync(Guid usuarioId) => await _context.Ofertas.Where(x => x.UsuarioId == usuarioId
                                                                                                                   && x.DataEHoraInclusao.Date == DateTime.Now.Date
                                                                                                                   && !x.Excluido)
                                                                                                               .CountAsync();

        public async Task<int> SomaDaQuantidadeTotalDaMoedaEmOfertasAsync(Guid moedaId) => await _context.Ofertas.Where(x => x.MoedaId == moedaId
                                                                                                                   && !x.Excluido)
                                                                                                                 .Select(y => y.Quantidade)
                                                                                                                 .SumAsync();
    }
}