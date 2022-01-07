using BackEnd.NetCore.Usuario.Commons.Models;
using BackEnd.NetCore.Usuario.Queries.DataContracts;
using System;

namespace BackEnd.NetCore.Usuario.Queries.Parsers
{
    public static class UsuarioQueryParser
    {
        public static ConsultarUsuarioQueryResponse ConverterParaResponse(UsuarioDAO usuario)
        {
            if (usuario == null)
                return null;

            return new ConsultarUsuarioQueryResponse()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Login = usuario.Login,
                Email = usuario.Email,
                Senha = usuario.Senha,
                CPF = usuario.CPF,
                CNPJ = usuario.CNPJ,
                Celular = usuario.Celular,
                DataCadastro = usuario.DataCadastro,
                Ativo = usuario.Ativo,
                Bloqueado = usuario.Bloqueado
            };
        }
    }
}
