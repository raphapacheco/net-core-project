using BackEnd.NetCore.Common.Repositories;
using BackEnd.NetCore.Usuario.Commons.Contexts;
using BackEnd.NetCore.Usuario.Commons.Models;
using BackEnd.NetCore.Usuario.Queries.DataContracts;
using BackEnd.NetCore.Usuario.Queries.Parsers;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.NetCore.Usuario.Queries.Handlers
{
    public class UsuarioQueryHandler :
        IRequestHandler<ConsultarUsuarioPorLoginQuery, ConsultarUsuarioQueryResponse>,
        IRequestHandler<ConsultarUsuarioPorIdQuery, ConsultarUsuarioQueryResponse>        
    {
        protected readonly Repository<UsuarioDAO> _repositorio;

        public UsuarioQueryHandler(UsuarioContext contexto)
        {
            _repositorio = new Repository<UsuarioDAO>(contexto);
        }

        public async Task<ConsultarUsuarioQueryResponse> Handle(ConsultarUsuarioPorLoginQuery query, CancellationToken cancellationToken)
        {
            if (!query.Valido(out var resultadoValidacao))
            {
                throw new ValidationException("Query inválida", resultadoValidacao.Errors);
            }

            var consulta = await _repositorio.GetByExpressionAsync(x => x.Login.Equals(query.Login.ToUpper()));

            var usuario = consulta.ToList().FirstOrDefault();
            
            return UsuarioQueryParser.ConverterParaResponse(usuario);
        }

        public async Task<ConsultarUsuarioQueryResponse> Handle(ConsultarUsuarioPorIdQuery query, CancellationToken cancellationToken)
        {
            if (!query.Valido(out var resultadoValidacao))
            {
                throw new ValidationException("Query inválida", resultadoValidacao.Errors);
            }

            var consulta = await _repositorio.GetByIdAsync(query.Id);

            return UsuarioQueryParser.ConverterParaResponse(consulta);
        }
    }
}
