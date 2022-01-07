using BackEnd.NetCore.Common.Utils;
using FluentValidation;

namespace BackEnd.NetCore.Usuario.Queries.DataContracts
{ 
    public class ConsultarUsuarioPorIdQueryValidator : AbstractValidator<ConsultarUsuarioPorIdQuery>
    {
        public ConsultarUsuarioPorIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationMessage.NOT_EMPTY);

            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage(ValidationMessage.NOT_NULL);
        }
    }
}
