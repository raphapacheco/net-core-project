using BackEnd.NetCore.Common.Utils;
using FluentValidation;

namespace BackEnd.NetCore.Usuario.Commons.Models
{
    internal class UsuarioValidador : AbstractValidator<UsuarioDAO>
    {
        internal UsuarioValidador()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(ValidationMessage.NOT_EMPTY);

            RuleFor(x => x.Nome)
                .MaximumLength(50)
                .WithMessage(ValidationMessage.MaximumLength(50));

            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage(ValidationMessage.NOT_EMPTY);

            RuleFor(x => x.Login)
                .MaximumLength(20)
                .WithMessage(ValidationMessage.MaximumLength(20));

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ValidationMessage.NOT_EMPTY);

            RuleFor(x => x.Email)
                .MaximumLength(50)
                .WithMessage(ValidationMessage.MaximumLength( 50));

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage(ValidationMessage.NOT_EMPTY);

            RuleFor(x => x.Senha)
                .MaximumLength(50)
                .WithMessage(ValidationMessage.MaximumLength(50));

            RuleFor(x => x.CPF)
                .MaximumLength(14)
                .WithMessage(ValidationMessage.MaximumLength(14));

            RuleFor(x => x.CNPJ)
                .MaximumLength(18)
                .WithMessage(ValidationMessage.MaximumLength(18));

            RuleFor(x => x.Celular)
                .NotEmpty()
                .WithMessage(ValidationMessage.NOT_EMPTY);

            RuleFor(x => x.Celular)
                .MaximumLength(17)
                .WithMessage(ValidationMessage.MaximumLength(17));
        }
    }
}