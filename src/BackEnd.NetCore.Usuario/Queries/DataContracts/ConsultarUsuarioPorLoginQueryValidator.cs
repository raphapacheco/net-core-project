using BackEnd.NetCore.Common.Utils;
using FluentValidation;

namespace BackEnd.NetCore.Usuario.Queries.DataContracts
{
    internal class ConsultarUsuarioPorLoginQueryValidator : AbstractValidator<ConsultarUsuarioPorLoginQuery>
    {
        public ConsultarUsuarioPorLoginQueryValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY);

            RuleFor(x => x.Login)
                .NotNull()
                .WithMessage(ValidationMessages.NOT_NULL);
        }
    }
}
