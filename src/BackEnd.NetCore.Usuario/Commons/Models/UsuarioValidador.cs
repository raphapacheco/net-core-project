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
                .WithMessage(ValidationMessage.NotEmpty("Nome"));

            RuleFor(x => x.Nome)
                .MaximumLength(50)
                .WithMessage(ValidationMessage.MaximumLength("Nome", 50));

            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty("Login"));

            RuleFor(x => x.Login)
                .MaximumLength(20)
                .WithMessage(ValidationMessage.MaximumLength("Login", 20));

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty("Email"));

            RuleFor(x => x.Email)
                .MaximumLength(50)
                .WithMessage(ValidationMessage.MaximumLength("Email", 50));

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty("Senha"));

            RuleFor(x => x.Senha)
                .MaximumLength(50)
                .WithMessage(ValidationMessage.MaximumLength("Senha", 50));

            RuleFor(x => x.CPF)
                .MaximumLength(14)
                .WithMessage(ValidationMessage.MaximumLength("CPF", 14));

            RuleFor(x => x.CNPJ)
                .MaximumLength(18)
                .WithMessage(ValidationMessage.MaximumLength("CNPJ", 18));

            RuleFor(x => x.Celular)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty("Celular"));

            RuleFor(x => x.Celular)
                .MaximumLength(17)
                .WithMessage(ValidationMessage.MaximumLength("Celular", 17));
        }
    }
}