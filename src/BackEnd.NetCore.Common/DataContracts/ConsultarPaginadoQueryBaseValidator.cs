using BackEnd.NetCore.Common.Utils;
using FluentValidation;

namespace BackEnd.NetCore.Common.DataContracts
{
    internal class ConsultarPaginadoQueryBaseValidator<TConsultarResponse> : AbstractValidator<ConsultarPaginadoQueryBase<TConsultarResponse>>
    {
        public ConsultarPaginadoQueryBaseValidator()
        {
            RuleFor(request => request.Pagina)
                .NotNull()
                .WithMessage(ValidationMessages.NOT_NULL);

            RuleFor(request => request.Tamanho)
                .NotNull()
                .WithMessage(ValidationMessages.NOT_NULL);

            RuleFor(request => request.Pagina)
                .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY);

            RuleFor(request => request.Tamanho)
                .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY);            
        }
    }
}
