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
        private readonly IMoedasService _moedasService;
        private readonly IOfertasRepository _ofertasRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<NovaOfertaInputDTO> _novaOfertaValidator;
        private readonly IValidator<ExcluirOfertaInputDTO> _excluirOfertaValidator;

        private const int LimiteDeOfertasPorDiaPorUsuario = 5;

        public OfertasService(IOfertasRepository ofertasRepository, IMapper mapper, IValidator<NovaOfertaInputDTO> novaOfertaValidator, IValidator<ExcluirOfertaInputDTO> excluirOfertaValidator, IMoedasService moedasService)
        {
            _ofertasRepository = ofertasRepository;
            _mapper = mapper;
            _novaOfertaValidator = novaOfertaValidator;
            _excluirOfertaValidator = excluirOfertaValidator;
            _moedasService = moedasService;
        }

        private async Task<bool> UsuarioAtingiuLimiteDeOfertasAsync(Guid usuarioId)
        {
            var result = await _ofertasRepository.QuantidadeOfertasPorDiaPorUsuarioAsync(usuarioId);

            if (result >= LimiteDeOfertasPorDiaPorUsuario)
                return true;

            return false;
        }

        private async Task<bool> ExisteSaldoParaCriacaoDaOfertaAsync(int quantidadeDaNovaOferta, int quantidadeTotalDisponivel, Guid moedaId)
        {
            var QuantidadeEmOfertas = await _ofertasRepository.SomaDaQuantidadeTotalDaMoedaEmOfertasAsync(moedaId);

            return quantidadeDaNovaOferta + QuantidadeEmOfertas <= quantidadeTotalDisponivel;
        }

        public async Task<OfertaOutputDTO> CriarOfertaAsync(NovaOfertaInputDTO inputDto)
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

            var dadosDaCarteira = await _moedasService.CarregarDadosDaCarteiraDoUsuarioAsync(inputDto.MoedaId, inputDto.UsuarioId);
            if (dadosDaCarteira is null)
            {
                var outputDto = new OfertaOutputDTO()
                {
                    MensagemDeRetorno = $"Não foi localizado a carteira virtual do usuário {inputDto.UsuarioId}.",
                    CodigoErro = (short)CodigoDeErros.Codigo.DadosFaltantesOuInvalidos,
                };

                return outputDto;
            }

            if (!await ExisteSaldoParaCriacaoDaOfertaAsync(inputDto.Quantidade, dadosDaCarteira.QuantidadeTotal, inputDto.MoedaId))
            {
                var outputDto = new OfertaOutputDTO()
                {
                    MensagemDeRetorno = $"Saldo desta moeda na carteira virtual do usuário é insuficiente.",
                    CodigoErro = (short)CodigoDeErros.Codigo.SaldoInsuficiente,
                };

                return outputDto;
            }

            if (await UsuarioAtingiuLimiteDeOfertasAsync(inputDto.UsuarioId))
            {
                var outputDto = new OfertaOutputDTO()
                {
                    MensagemDeRetorno = "O usuário atingiu o limite de ofertas permitidas por dia.",
                    CodigoErro = (short)CodigoDeErros.Codigo.LimiteDeOfertas,
                };

                return outputDto;
            }

            var oferta = _mapper.Map<Oferta>(inputDto);
            oferta.DataEHoraInclusao = DateTime.Now;

            var idOfertaCriada = await _ofertasRepository.CriarOfertaAsync(oferta);
            oferta.Id = idOfertaCriada;

            var output = _mapper.Map<OfertaOutputDTO>(oferta);
            output.MensagemDeRetorno = "Cadastrado com sucesso!";

            return output;
        }

        public async Task<OfertaOutputDTO> ExcluirOfertaAsync(ExcluirOfertaInputDTO inputDto)
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

            var oferta = await _ofertasRepository.LocalizarOfertaByIdAsync(inputDto.Id);

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
            oferta.DataEHoraExclusao = DateTime.Now;
            await _ofertasRepository.AtualizarOfertaAsync(oferta);

            var output = _mapper.Map<OfertaOutputDTO>(oferta);
            output.MensagemDeRetorno = "Excluído com sucesso!";

            return output;
        }

        public async Task<IList<Oferta>> GetBalcaoDeOfertasAsync(int page, int pageSize, string? scrollIdString)
        {
            if (int.TryParse(scrollIdString, out var scrollId))
                return await _ofertasRepository.GetBalcaoDeOfertasByScrollAsync(scrollId, pageSize);

            return await _ofertasRepository.GetBalcaoDeOfertasByPageAsync(page, pageSize);
        }
    }
}