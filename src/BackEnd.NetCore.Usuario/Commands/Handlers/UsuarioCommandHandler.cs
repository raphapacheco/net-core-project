using BackEnd.NetCore.Common.DataContracts;
using BackEnd.NetCore.Common.Generics.Services;
using BackEnd.NetCore.Common.Utils;
using BackEnd.NetCore.Usuario.Commands.DataContracts;
using BackEnd.NetCore.Usuario.Commands.Parsers;
using BackEnd.NetCore.Usuario.Commons.Contexts;
using BackEnd.NetCore.Usuario.Commons.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.NetCore.Usuario.Commands.Handlers
{
    public class UsuarioCommandHandler :
        IRequestHandler<InserirUsuarioCommand, ResponseBase>,
        IRequestHandler<AtualizarUsuarioCommand, ResponseBase>,
        IRequestHandler<ExcluirUsuarioCommand, ResponseBase>
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
                throw new ValidationMessage("Command inválido", resultadoValidacao.Errors);
            }

           var id = await _servico.InserirAsync(UsuarioCommandParser.ConverterParaModelo(command));

           return new ResponseBase() 
           { 
               Id = id,
               Mensagem = "Usuário inserido com sucesso"
           };
        }

        public async Task<ResponseBase> Handle(ExcluirUsuarioCommand command, CancellationToken cancellationToken)
        {
            if (!command.Valido(out var resultadoValidacao))
            {
                throw new ValidationMessage("Command inválido", resultadoValidacao.Errors);
            }

            var consulta = await _servico.ConsultarPorIdentificadorAsync(command.Id);

            if (consulta == null)
            {
                return null;
            }

            await _servico.ExcluirAsync(command.Id);

            return new ResponseBase() 
            { 
                Id = command.Id,                
                Mensagem = "Usuário excluído com sucesso"
            };
        }

        public async Task<ResponseBase> Handle(AtualizarUsuarioCommand command, CancellationToken cancellationToken)
        {
            if (!command.Valido(out var resultadoValidacao))
            {
                throw new ValidationMessage("Command inválido", resultadoValidacao.Errors);
            }

            var consulta = await _servico.ConsultarPorIdentificadorAsync(command.Id);

            if (consulta == null)
            {
                return null;
            }

            await _servico.AtualizarAsync(UsuarioCommandParser.ConverterParaModelo(command));

            return new ResponseBase() 
            { 
                Id = command.Id,
                Mensagem = "Usuario atualizado com sucesso"
            };
        }
    }
}
