using BackEnd.NetCore.Common.DataContracts;
using BackEnd.NetCore.Common.Services;
using BackEnd.NetCore.Usuario.Commands.DataContracts;
using BackEnd.NetCore.Usuario.Commands.Parsers;
using BackEnd.NetCore.Usuario.Commons.Contexts;
using BackEnd.NetCore.Usuario.Commons.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.NetCore.Usuario.Commands.Handlers
{
    public class UsuarioCommandHandler : IRequestHandler<InserirUsuarioCommand, ResponseBase>
    {
        protected readonly Service<UsuarioDAO> _servico;

        public UsuarioCommandHandler(UsuarioContext contexto)
        {
            _servico = new Service<UsuarioDAO>(contexto);
        }

        public async Task<ResponseBase> Handle(InserirUsuarioCommand command, CancellationToken cancellationToken)
        {
            if (!command.Valido(out var resultadoValidacao))
            {
                throw new Exception($"Command inválido: { string.Join("\n\n", resultadoValidacao)}");
            }

           var id = await _servico.InserirAsync(UsuarioCommandParser.ConverterParaModelo(command));

           return new ResponseBase() { Id = id };
        }
    }
}
