using BackEnd.NetCore.Common.Models;
using FluentValidation.Results;

namespace BackEnd.NetCore.Common.TesteUnitario.ValueObjects.Stubs
{
    public class Usuario : Model
    {
        public Usuario(string login, string senha)
        {
            Login = login;
            Senha = senha;
        }

        public string Senha { get; set; }

        public string Login { get; set; }

        public override bool Valido(out ValidationResult resultadoValidacao)
        {
            resultadoValidacao = null;
            return true;
        }
    }
}
