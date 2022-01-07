using BackEnd.NetCore.Common.Utils;
using FluentValidation;

namespace BackEnd.NetCore.Usuario.Queries.DataContracts
{
    public class ConsultarUsuarioPorLoginQueryValidator : AbstractValidator<ConsultarUsuarioPorLoginQuery>
    {
        public ConsultarUsuarioPorLoginQueryValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty("Login"));
        }
    }
}
