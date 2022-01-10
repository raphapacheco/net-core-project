using BackEnd.NetCore.Common.Utils;
using FluentValidation;

namespace BackEnd.NetCore.Common.DataContracts
{
    public class ConsultarPaginadoQueryBaseValidator<TConsultarResponse> : AbstractValidator<ConsultarPaginadoQueryBase<TConsultarResponse>>
    {
        public ConsultarPaginadoQueryBaseValidator()
        {
            RuleFor(request => request.Pagina)
                .NotNull()
                .WithMessage(ValidationMessage.NOT_NULL);

            RuleFor(request => request.Tamanho)
                .NotNull()
                .WithMessage(ValidationMessage.NOT_NULL);

            RuleFor(request => request.Pagina)
                .NotEmpty()
                .WithMessage(ValidationMessage.NOT_EMPTY);

            RuleFor(request => request.Tamanho)
                .NotEmpty()
                .WithMessage(ValidationMessage.NOT_EMPTY);            
        }
    }
}
