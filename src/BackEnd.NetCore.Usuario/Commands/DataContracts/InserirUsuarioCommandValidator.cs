using BackEnd.NetCore.Common.Utils;
using FluentValidation;

namespace BackEnd.NetCore.Usuario.Commands.DataContracts
{
    public class InserirUsuarioCommandValidator : AbstractValidator<InserirUsuarioCommand>
    {
        public InserirUsuarioCommandValidator()
        {
            RuleFor(x => x.CPF)
                .Must(cpf => CPFValidator.Valido(cpf))
                .When(x => !string.IsNullOrEmpty(x.CPF))
                .WithMessage(ValidationMessage.Invalid("CPF"));

            RuleFor(x => x.CNPJ)
                .Must(cnpj => CPFValidator.Valido(cnpj))
                .When(x => !string.IsNullOrEmpty(x.CNPJ))
                .WithMessage(ValidationMessage.Invalid("CNPJ"));
        }
    }
}
