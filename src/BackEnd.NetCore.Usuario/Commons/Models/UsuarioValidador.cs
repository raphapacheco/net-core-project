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
                .WithMessage(ValidationMessages.NOT_EMPTY);

            RuleFor(x => x.Nome)
                .MaximumLength(50)
                .WithMessage(ValidationMessages.MaximumLength(50));

            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY);

            RuleFor(x => x.Login)
                .MaximumLength(20)
                .WithMessage(ValidationMessages.MaximumLength(20));

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY);

            RuleFor(x => x.Email)
                .MaximumLength(50)
                .WithMessage(ValidationMessages.MaximumLength( 50));

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY);

            RuleFor(x => x.Senha)
                .MaximumLength(50)
                .WithMessage(ValidationMessages.MaximumLength(50));

            RuleFor(x => x.CPF)
                .MaximumLength(14)
                .WithMessage(ValidationMessages.MaximumLength(14));

            RuleFor(x => x.CNPJ)
                .MaximumLength(18)
                .WithMessage(ValidationMessages.MaximumLength(18));

            RuleFor(x => x.Celular)
                .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY);

            RuleFor(x => x.Celular)
                .MaximumLength(17)
                .WithMessage(ValidationMessages.MaximumLength(17));
        }
    }
}