using BackEnd.NetCore.Common.Repositories;
using BackEnd.NetCore.Usuario.Commons.Contexts;
using BackEnd.NetCore.Usuario.Commons.Models;
using BackEnd.NetCore.Usuario.Queries.DataContracts;
using BackEnd.NetCore.Usuario.Queries.Parsers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.NetCore.Usuario.Queries.Handlers
{
    public class UsuarioQueryHandler : IRequestHandler<ConsultarUsuarioQuery, ConsultarUsuarioQueryResponse>
    {
        protected readonly Repository<UsuarioDAO> _repositorio;

        public UsuarioQueryHandler(UsuarioContext contexto)
        {
            _repositorio = new Repository<UsuarioDAO>(contexto);
        }

        public async Task<ConsultarUsuarioQueryResponse> Handle(ConsultarUsuarioQuery query, CancellationToken cancellationToken)
        {
            if (!query.Valido(out var resultadoValidacao))
            {
                throw new Exception($"Query inválida: { string.Join("\n\n", resultadoValidacao)}");
            }

            var consulta = await _repositorio.GetByExpressionAsync(x => x.Login.Equals(query.Login));

            var usuario = consulta.ToList().FirstOrDefault();
            
            return UsuarioQueryParser.ConverterParaResponse(usuario);
        }
    }
}
