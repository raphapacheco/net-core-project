using BackEnd.NetCore.Common.Utils;
using FluentValidation;

namespace BackEnd.NetCore.Common.DataContracts
{
    public class ConsultarPorIdQueryBaseValidator<TResponse> : AbstractValidator<ConsultarPorIdQueryBase<TResponse>>
    {
        public ConsultarPorIdQueryBaseValidator()
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
