using BackEnd.NetCore.Common.Utils;
using BackEnd.NetCore.Usuario.Commands.DataContracts;
using BackEnd.NetCore.Usuario.Commons.Models;

namespace BackEnd.NetCore.Usuario.Commands.Parsers
{
    public static class UsuarioCommandParser
    {
        public static UsuarioDAO ConverterParaModelo(InserirUsuarioCommand command)
        {  
            return new UsuarioDAO()
            {
                Nome = command.Nome,
                Login = command.Login.ToUpper(),
                Email = command.Email.ToLower(),
                Senha = TripleDes.Encrypt(Secret.GetSecretAsByteArray(), command.Senha),
                CPF = command.CPF,
                CNPJ = command.CNPJ,
                Celular = command.Celular,        
            };
        }
    }
}
