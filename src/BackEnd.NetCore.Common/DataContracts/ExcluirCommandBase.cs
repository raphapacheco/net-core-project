using FluentValidation.Results;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace BackEnd.NetCore.Common.DataContracts
{
    [ExcludeFromCodeCoverage]
    public abstract class ExcluirCommandBase : IRequest<ResponseBase>
    {
        public int Id { get; set; }

        public bool Valido(out ValidationResult resultadoValidacao)
        {
            resultadoValidacao = new ExcluirCommandBaseValidator().Validate(this);
            return resultadoValidacao.IsValid;
        }
    }
}
