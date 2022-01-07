using FluentValidation.Results;
using MediatR;

namespace BackEnd.NetCore.Usuario.Queries.DataContracts
{
    public class ConsultarUsuarioPorIdQuery : IRequest<ConsultarUsuarioQueryResponse>
    {
        public int Id { get; set; }

        public bool Valido(out ValidationResult resultadoValidacao)
        {
            resultadoValidacao = new ConsultarUsuarioPorIdQueryValidator().Validate(this);
            return resultadoValidacao.IsValid;
        }
    }
}
