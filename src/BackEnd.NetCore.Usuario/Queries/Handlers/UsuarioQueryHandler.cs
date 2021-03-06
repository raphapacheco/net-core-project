using BackEnd.NetCore.Common.Generics.Repositories;
using BackEnd.NetCore.Common.Utils;
using BackEnd.NetCore.Common.ValueObjects;
using BackEnd.NetCore.Usuario.Commons.Contexts;
using BackEnd.NetCore.Usuario.Commons.Models;
using BackEnd.NetCore.Usuario.Queries.DataContracts;
using BackEnd.NetCore.Usuario.Queries.Parsers;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.NetCore.Usuario.Queries.Handlers
{
    public class UsuarioQueryHandler :
        IRequestHandler<ConsultarUsuarioPorIdQuery, ConsultarUsuarioResponse>,
        IRequestHandler<ConsultarUsuarioPorLoginQuery, ConsultarUsuarioPorLoginResponse>,        
        IRequestHandler<ConsultarPaginadoUsuarioQuery, ConsultarPaginadoUsuarioResponse>        
    {
        protected readonly Repository<UsuarioDAO> _repositorio;

        public UsuarioQueryHandler(UsuarioContext contexto)
        {
            _repositorio = new Repository<UsuarioDAO>(contexto);
        }        

        public async Task<ConsultarUsuarioResponse> Handle(ConsultarUsuarioPorIdQuery query, CancellationToken cancellationToken)
        {
            if (!query.Valido(out var resultadoValidacao))
            {
                throw new ValidationMessage("Query inválida", resultadoValidacao.Errors);
            }

            var consulta = await _repositorio.GetByIdAsync(query.Id);

            return UsuarioQueryParser.ConverterParaResponse(consulta);
        }

        public async Task<ConsultarUsuarioPorLoginResponse> Handle(ConsultarUsuarioPorLoginQuery query, CancellationToken cancellationToken)
        {
            if (!query.Valido(out var resultadoValidacao))
            {
                throw new ValidationMessage("Query inválida", resultadoValidacao.Errors);
            }

            var consulta = await _repositorio.GetByExpressionAsync(x => x.Login.Equals(query.Login.ToUpper()));

            var usuario = consulta.ToList().FirstOrDefault();

            return UsuarioQueryParser.ConverterParaConsultarUsuarioPorLoginResponse(usuario);
        }

        public virtual async Task<ConsultarPaginadoUsuarioResponse> Handle(ConsultarPaginadoUsuarioQuery query, CancellationToken cancellationToken)
        {
            if (!query.Valido(out var resultadoValidacao))
            {
                throw new ValidationMessage("Query inválida", resultadoValidacao.Errors);
            }

            var consulta = await _repositorio.GetAllAsync(new Pagination(query.Pagina, query.Tamanho));

            return UsuarioQueryParser.ConverterParaConsultarPaginadoResponse(consulta);
        }
    }
}
