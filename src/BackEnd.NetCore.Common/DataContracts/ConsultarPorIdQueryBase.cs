using FluentValidation.Results;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace BackEnd.NetCore.Common.DataContracts
{
    [ExcludeFromCodeCoverage]
    public abstract class ConsultarPorIdQueryBase<TResponse> : IRequest<TResponse>
    {
        public int Id { get; set; }

        public bool Valido(out ValidationResult resultadoValidacao)
        {
            resultadoValidacao = new ConsultarPorIdQueryBaseValidator<TResponse>().Validate(this);
            return resultadoValidacao.IsValid;
        }
    }
}
