using FluentValidation.Results;
using MediatR;

namespace BackEnd.NetCore.Usuario.Queries.DataContracts
{
    public class ConsultarUsuarioQuery : IRequest<ConsultarUsuarioQueryResponse>
    {
        public string Login { get; set; }

        public bool Valido(out ValidationResult resultadoValidacao)
        {
            resultadoValidacao = new ConsultarUsuarioQueryValidator().Validate(this);
            return resultadoValidacao.IsValid;
        }
    }
}
