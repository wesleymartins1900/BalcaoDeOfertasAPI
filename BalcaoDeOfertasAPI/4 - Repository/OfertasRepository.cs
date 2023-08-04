using DtosBalcaoDeOfertas.InputDTO;
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

        public async Task<long> CriarOferta(Oferta oferta)
        {
            var result = await _context.Ofertas.AddAsync(oferta);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task AtualizarOferta(Oferta oferta)
        {
            _context.Update(oferta);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteSaldoParaCriacaoDaOferta(NovaOfertaInputDTO inputDto)
        {
            var saldoTotalDisponivel = await _context.Moedas.Where(x => x.Id == inputDto.MoedaId)
                                                            .Select(x => x.QuantidadeTotal)
                                                            .FirstOrDefaultAsync();

            if (saldoTotalDisponivel is (int)default)
                return false;

            var saldoTotalEmOferta = await _context.Ofertas.Where(x => x.MoedaId == inputDto.MoedaId
                                                                              && x.UsuarioId == inputDto.UsuarioId
                                                                              && !x.Excluido)
                                                                     .Select(x => x.Quantidade)
                                                                     .SumAsync();

            return saldoTotalEmOferta + inputDto.Quantidade <= saldoTotalDisponivel;
        }

        public async Task<IList<Oferta>> GetBalcaoDeOfertasByPage(int page, int pageSize) => await _context.Ofertas.Where(x => !x.Excluido)
                                                                                                                   .Skip(page)
                                                                                                                   .Take(pageSize)
                                                                                                                   .OrderByDescending(x => x.DataEHoraInclusao)
                                                                                                                   .ToListAsync();

        public async Task<IList<Oferta>> GetBalcaoDeOfertasByScroll(int scrollId, int pageSize) => await _context.Ofertas.Where(x => x.Id > scrollId
                                                                                                                                  && !x.Excluido)
                                                                                                                         .Take(pageSize)
                                                                                                                         .OrderByDescending(x => x.DataEHoraInclusao)
                                                                                                                         .ToListAsync();

        public async Task<Oferta?> LocalizarOfertaById(long id) => await _context.Ofertas.FindAsync(id);

        public async Task<int> QuantidadeOfertasPorDiaPorUsuario(Guid usuarioId) => await _context.Ofertas.Where(x => x.UsuarioId == usuarioId
                                                                                                                   && x.DataEHoraInclusao.Date == DateTime.Now.Date
                                                                                                                   && !x.Excluido)
                                                                                                          .CountAsync();
    }
}