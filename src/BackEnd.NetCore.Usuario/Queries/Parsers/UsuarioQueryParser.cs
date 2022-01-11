using BackEnd.NetCore.Common.ValueObjects;
using BackEnd.NetCore.Usuario.Commons.Models;
using BackEnd.NetCore.Usuario.Queries.DataContracts;
using System.Collections.Generic;

namespace BackEnd.NetCore.Usuario.Queries.Parsers
{
    internal static class UsuarioQueryParser
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

        public static ConsultarPaginadoUsuarioResponse ConverterParaConsultarPaginadoResponse(PaginationResponse<UsuarioDAO> paginationResponse)
        {
            var itens = new List<ConsultarUsuarioQueryResponse>();

            foreach (var entidade in paginationResponse.Items)
            {
                itens.Add(ConverterParaResponse(entidade));
            }

            return new ConsultarPaginadoUsuarioResponse()
            {
                Pagina = paginationResponse.Page,
                Itens = itens
            };
        }        
    }
}
