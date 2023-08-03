using AutoMapper;
using DtosBalcaoDeOfertas.InputDTO;
using BalcaoDeOfertasAPI._1___Models;
using BalcaoDeOfertasAPI._0___Config.Utils;
using BalcaoDeOfertasAPI._3___Services.Interfaces;
using BalcaoDeOfertasAPI._4___Repository.Interfaces;

namespace BalcaoDeOfertasAPI._3___Services
{
    public class OfertasService : IOfertasService
    {
        private readonly IOfertasRepository _balcaoDeOfertasRepository;
        private readonly IMapper _mapper;

        private const int LimiteDeOfertasPorDiaPorUsuario = 5;

        public OfertasService(IOfertasRepository balcaoDeOfertasRepository, IMapper mapper)
        {
            _balcaoDeOfertasRepository = balcaoDeOfertasRepository;
            _mapper = mapper;
        }

        private async Task<bool> ExisteOferta(long id)
        {
            // ToDo: Alterar verificação de duplicados

            var result = await _balcaoDeOfertasRepository.LocalizarOfertaById(id);
            return result is not null;
        }

        private async Task<bool> UsuarioAtingiuLimiteDeOfertas(Guid usuarioId)
        {
            var result = await _balcaoDeOfertasRepository.QuantidadeOfertasPorDiaPorUsuario(usuarioId);

            if (result >= LimiteDeOfertasPorDiaPorUsuario)
                return true;

            return false;
        }

        public async Task<OfertaInputDTO> CriarOferta(OfertaInputDTO inputDto)
        {
            if (await ExisteOferta(inputDto.Id))
            {
                // ToDo: Ajustar texto depois que alterar a verificação de duplicados

                inputDto.Erro = $"Já existe uma oferta com o Id {inputDto.Id}.";
                inputDto.CodigoErro = (short)CodigoDeErros.Codigo.Duplicado;
                return inputDto;
            }

            if (await UsuarioAtingiuLimiteDeOfertas(inputDto.UsuarioId))
            {
                inputDto.Erro = "O usuário atingiu o limite permitido de ofertas por dia.";
                inputDto.CodigoErro = (short)CodigoDeErros.Codigo.LimiteDeOfertas;
                return inputDto;
            }

            var oferta = _mapper.Map<Oferta>(inputDto);
            await _balcaoDeOfertasRepository.CriarOferta(oferta);
            return inputDto;
        }

        public async Task<OfertaInputDTO> ExcluirOferta(OfertaInputDTO inputDto)
        {
            var oferta = await _balcaoDeOfertasRepository.LocalizarOfertaById(inputDto.Id);

            if (oferta is null)
            {
                inputDto.Erro = $"Não foi localizado uma oferta com o Id {inputDto.Id}";
                inputDto.CodigoErro = (short)CodigoDeErros.Codigo.NaoLocalizado;
                return inputDto;
            }

            if (inputDto.UsuarioId != oferta.UsuarioId)
            {
                inputDto.Erro = $"A oferta com Id {inputDto.Id} não pertence ao usuário informado.";
                inputDto.CodigoErro = (short)CodigoDeErros.Codigo.UsuarioIncorreto;
                return inputDto;
            }

            oferta.Excluido = true;

            await _balcaoDeOfertasRepository.AtualizarOferta(oferta);
            return inputDto;
        }

        public async Task<IList<Oferta>> GetBalcaoDeOfertas(int page, int pageSize, string? scrollIdString)
        {
            if (int.TryParse(scrollIdString, out var scrollId))
                return await _balcaoDeOfertasRepository.GetBalcaoDeOfertasByScroll(scrollId, pageSize);

            return await _balcaoDeOfertasRepository.GetBalcaoDeOfertasByPage(page, pageSize);
        }
    }
}