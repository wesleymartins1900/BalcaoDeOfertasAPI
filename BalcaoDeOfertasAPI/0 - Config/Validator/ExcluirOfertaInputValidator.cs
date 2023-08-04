using FluentValidation;
using DtosBalcaoDeOfertas.InputDTO;

namespace BalcaoDeOfertasAPI._0___Config.Validator
{
    public class ExcluirOfertaInputValidator : AbstractValidator<ExcluirOfertaInputDTO>
    {
        public ExcluirOfertaInputValidator()
        {
            RuleFor(oferta => oferta.Id).NotNull()
                                        .NotEmpty()
                                        .WithMessage("O campo Id é obrigatório e precisa ser válido.");

            RuleFor(oferta => oferta.UsuarioId).NotNull()
                                               .NotEmpty()
                                               .WithMessage("O campo UsuarioId é obrigatório e precisa ser de um usuário válido.");
        }
    }
}