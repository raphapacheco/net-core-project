using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;
using MediatR;

namespace BackEnd.NetCore.Common.DataContracts
{
    [ExcludeFromCodeCoverage]
    public abstract class ConsultarPaginadoQueryBase<TResponse> : IRequest<TResponse>
    {
        public int Pagina { get; set; }
        public int Tamanho { get; set; }

        public virtual bool Valido(out ValidationResult resultadoValidacao)
        {
            resultadoValidacao = new ConsultarPaginadoQueryBaseValidator<TResponse>().Validate(this);
            return resultadoValidacao.IsValid;
        }
    }
}
