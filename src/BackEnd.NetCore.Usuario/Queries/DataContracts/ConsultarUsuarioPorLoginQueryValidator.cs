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
                .WithMessage(ValidationMessage.NOT_EMPTY);

            RuleFor(x => x.Login)
                .NotNull()
                .WithMessage(ValidationMessage.NOT_NULL);
        }
    }
}
