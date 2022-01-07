using BackEnd.NetCore.Common.Utils;
using FluentValidation;

namespace BackEnd.NetCore.Usuario.Queries.DataContracts
{
    public class ConsultarUsuarioQueryValidator : AbstractValidator<ConsultarUsuarioQuery>
    {
        public ConsultarUsuarioQueryValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty("Login"));
        }
    }
}
