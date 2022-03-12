using FluentValidation.Results;
using MediatR;

namespace BackEnd.NetCore.Usuario.Queries.DataContracts
{
    public class ConsultarUsuarioPorLoginQuery : IRequest<ConsultarUsuarioPorLoginResponse>
    {
        public string Login { get; set; }
        public string Senha { get; set; }        

        public bool Valido(out ValidationResult resultadoValidacao)
        {
            resultadoValidacao = new ConsultarUsuarioPorLoginQueryValidator().Validate(this);
            return resultadoValidacao.IsValid;
        }
    }
}
