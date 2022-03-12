using BackEnd.NetCore.Common.ValueObjects;
using BackEnd.NetCore.Usuario.Commons.Models;
using BackEnd.NetCore.Usuario.Queries.DataContracts;
using System.Collections.Generic;

namespace BackEnd.NetCore.Usuario.Queries.Parsers
{
    internal static class UsuarioQueryParser
    {
        public static ConsultarUsuarioResponse ConverterParaResponse(UsuarioDAO usuario)
        {
            if (usuario == null)
                return null;

            return new ConsultarUsuarioResponse()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Login = usuario.Login,
                Email = usuario.Email,
                CPF = usuario.CPF,
                CNPJ = usuario.CNPJ,
                Celular = usuario.Celular,
                DataCadastro = usuario.DataCadastro,
                Ativo = usuario.Ativo
            };
        }

        public static ConsultarUsuarioPorLoginResponse ConverterParaConsultarUsuarioPorLoginResponse(UsuarioDAO usuario)
        {
            if (usuario == null)
                return null;

            return new ConsultarUsuarioPorLoginResponse()
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
                Ativo = usuario.Ativo
            };
        }

        public static ConsultarPaginadoUsuarioResponse ConverterParaConsultarPaginadoResponse(PaginationResponse<UsuarioDAO> paginationResponse)
        {
            var itens = new List<ConsultarUsuarioResponse>();

            foreach (var modelo in paginationResponse.Items)
            {
                itens.Add(ConverterParaResponse(modelo));
            }

            return new ConsultarPaginadoUsuarioResponse()
            {
                Pagina = paginationResponse.Page,
                Itens = itens,
                Tamanho = paginationResponse.Size,
                TotalPaginas = paginationResponse.TotalPages
            };
        }        
    }
}
