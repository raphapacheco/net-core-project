using FluentValidation.Results;
using MediatR;

namespace BackEnd.NetCore.Usuario.Queries.DataContracts
{
    public class ConsultarUsuarioPorLoginQuery : IRequest<ConsultarUsuarioQueryResponse>
    {
        public string Login { get; set; }

        public bool Valido(out ValidationResult resultadoValidacao)
        {
            resultadoValidacao = new ConsultarUsuarioPorLoginQueryValidator().Validate(this);
            return resultadoValidacao.IsValid;
        }
    }
}
