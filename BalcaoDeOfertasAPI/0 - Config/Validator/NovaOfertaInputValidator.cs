using FluentValidation;
using DtosBalcaoDeOfertas.InputDTO;

namespace BalcaoDeOfertasAPI._0___Config.Validator
{
    public class NovaOfertaInputValidator : AbstractValidator<NovaOfertaInputDTO>
    {
        public NovaOfertaInputValidator()
        {
            RuleFor(oferta => oferta.UsuarioId).NotNull()
                                               .NotEmpty()
                                               .WithMessage("O campo UsuarioId é obrigatório e deve ser de um usuário válido.");

            RuleFor(oferta => oferta.Quantidade).GreaterThan(0)
                                                .WithMessage("O campo Quantidade é obrigatório e deve ser maior que zero.");

            RuleFor(oferta => oferta.PrecoUnitario).GreaterThan(0)
                                                   .WithMessage("O campo Quantidade é obrigatório e deve ser maior que zero.");
        }
    }
}