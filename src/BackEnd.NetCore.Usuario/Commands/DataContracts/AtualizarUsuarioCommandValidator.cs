using BackEnd.NetCore.Common.Utils;
using FluentValidation;

namespace BackEnd.NetCore.Usuario.Commands.DataContracts
{
    internal class AtualizarUsuarioCommandValidator : AbstractValidator<AtualizarUsuarioCommand>
    {
        public AtualizarUsuarioCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage(ValidationMessage.NOT_NULL);

            RuleFor(x => x.Nome)
                .NotNull()
                .WithMessage(ValidationMessage.NOT_NULL);
           
            RuleFor(x => x.Login)
                .NotNull()
                .WithMessage(ValidationMessage.NOT_NULL);
          
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage(ValidationMessage.NOT_NULL);
         
            RuleFor(x => x.Senha)
                .NotNull()
                .WithMessage(ValidationMessage.NOT_NULL);               

            RuleFor(x => x.Celular)
                .NotNull()
                .WithMessage(ValidationMessage.NOT_NULL);

            RuleFor(x => x.CPF)
                .Must(cpf => CPFValidator.Valido(cpf))
                .When(x => !string.IsNullOrEmpty(x.CPF))
                .WithMessage(ValidationMessage.INVALID);

            RuleFor(x => x.CNPJ)
                .Must(cnpj => CPFValidator.Valido(cnpj))
                .When(x => !string.IsNullOrEmpty(x.CNPJ))
                .WithMessage(ValidationMessage.INVALID);
        }
    }
}
