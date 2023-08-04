using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<NovaOfertaInputDTO> _novaOfertaValidator;
        private readonly IValidator<ExcluirOfertaInputDTO> _excluirOfertaValidator;

        private const int LimiteDeOfertasPorDiaPorUsuario = 5;

        public OfertasService(IOfertasRepository balcaoDeOfertasRepository, IMapper mapper, IValidator<NovaOfertaInputDTO> novaOfertaValidator, IValidator<ExcluirOfertaInputDTO> excluirOfertaValidator)
        {
            _balcaoDeOfertasRepository = balcaoDeOfertasRepository;
            _mapper = mapper;
            _novaOfertaValidator = novaOfertaValidator;
            _excluirOfertaValidator = excluirOfertaValidator;
        }

        private async Task<bool> UsuarioAtingiuLimiteDeOfertas(Guid usuarioId)
        {
            var result = await _balcaoDeOfertasRepository.QuantidadeOfertasPorDiaPorUsuario(usuarioId);

            if (result >= LimiteDeOfertasPorDiaPorUsuario)
                return true;

            return false;
        }

        private async Task<bool> ExisteSaldoParaCriacaoDaOferta(NovaOfertaInputDTO inputDto) => await _balcaoDeOfertasRepository.ExisteSaldoParaCriacaoDaOferta(inputDto);

        public async Task<OfertaOutputDTO> CriarOferta(NovaOfertaInputDTO inputDto)
        {
            var validationResult = await _novaOfertaValidator.ValidateAsync(inputDto);

            if (!validationResult.IsValid)
            {
                var outputDto = new OfertaOutputDTO()
                {
                    MensagemDeRetorno = "Dados faltantes ou inválidos.",
                    ErrosDeValidacao = validationResult.Errors,
                    CodigoErro = (short)CodigoDeErros.Codigo.DadosFaltantesOuInvalidos,
                };

                return outputDto;
            }

            if (await UsuarioAtingiuLimiteDeOfertas(inputDto.UsuarioId))
            {
                var outputDto = new OfertaOutputDTO()
                {
                    MensagemDeRetorno = "O usuário atingiu o limite de ofertas permitidas por dia.",
                    CodigoErro = (short)CodigoDeErros.Codigo.LimiteDeOfertas,
                };

                return outputDto;
            }

            if (await ExisteSaldoParaCriacaoDaOferta(inputDto))
            {
                var outputDto = new OfertaOutputDTO()
                {
                    MensagemDeRetorno = $"Saldo desta moeda na carteira virtual do usuário é insuficiente.",
                    CodigoErro = (short)CodigoDeErros.Codigo.SaldoInsuficiente,
                };

                return outputDto;
            }

            var oferta = _mapper.Map<Oferta>(inputDto);
            var idOfertaCriada = await _balcaoDeOfertasRepository.CriarOferta(oferta);

            oferta.Id = idOfertaCriada;

            var output = _mapper.Map<OfertaOutputDTO>(oferta);
            output.MensagemDeRetorno = "Cadastrado com sucesso!";

            return output;
        }

        public async Task<OfertaOutputDTO> ExcluirOferta(ExcluirOfertaInputDTO inputDto)
        {
            var validationResult = await _excluirOfertaValidator.ValidateAsync(inputDto);

            if (!validationResult.IsValid)
            {
                var outputDto = new OfertaOutputDTO()
                {
                    MensagemDeRetorno = "Dados faltantes ou inválidos.",
                    ErrosDeValidacao = validationResult.Errors,
                    CodigoErro = (short)CodigoDeErros.Codigo.DadosFaltantesOuInvalidos,
                };

                return outputDto;
            }

            var oferta = await _balcaoDeOfertasRepository.LocalizarOfertaById(inputDto.Id);

            if (oferta is null)
            {
                var outputDto = new OfertaOutputDTO()
                {
                    MensagemDeRetorno = $"Não foi localizado uma oferta com o Id {inputDto.Id}",
                    CodigoErro = (short)CodigoDeErros.Codigo.NaoLocalizado,
                };

                return outputDto;
            }

            if (oferta.Excluido)
            {
                var outputDto = new OfertaOutputDTO()
                {
                    MensagemDeRetorno = $"A oferta com o Id {inputDto.Id} já foi excluída.",
                    CodigoErro = (short)CodigoDeErros.Codigo.Excluido,
                };

                return outputDto;
            }

            if (inputDto.UsuarioId != oferta.UsuarioId)
            {
                var outputDto = new OfertaOutputDTO()
                {
                    MensagemDeRetorno = $"A oferta com Id {inputDto.Id} não pertence ao usuário informado.",
                    CodigoErro = (short)CodigoDeErros.Codigo.UsuarioIncorreto,
                };

                return outputDto;
            }

            oferta.Excluido = true;
            await _balcaoDeOfertasRepository.AtualizarOferta(oferta);

            var output = _mapper.Map<OfertaOutputDTO>(oferta);
            output.MensagemDeRetorno = "Excluído com sucesso!";

            return output;
        }

        public async Task<IList<Oferta>> GetBalcaoDeOfertas(int page, int pageSize, string? scrollIdString)
        {
            if (int.TryParse(scrollIdString, out var scrollId))
                return await _balcaoDeOfertasRepository.GetBalcaoDeOfertasByScroll(scrollId, pageSize);

            return await _balcaoDeOfertasRepository.GetBalcaoDeOfertasByPage(page, pageSize);
        }
    }
}