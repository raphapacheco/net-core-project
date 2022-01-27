using BackEnd.NetCore.Usuario.Commands.DataContracts;
using BackEnd.NetCore.Usuario.Commands.Handlers;
using BackEnd.NetCore.Usuario.Commons.Contexts;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;
using Microsoft.EntityFrameworkCore;
using BackEnd.NetCore.Common.Utils;

namespace BackEnd.NetCore.Usuario.TesteUnitario.Commands.Handlers
{
    public class UsuarioCommandHandlerTeste
    {
        private UsuarioCommandHandler _handler;
        private readonly CancellationToken _cancellationToken;

        public UsuarioCommandHandlerTeste()
        {
            var options = new Mock<DbContextOptions<UsuarioContext>>()
            {
                CallBase = true
            };

            _handler = new UsuarioCommandHandler(new UsuarioContext(options.Object));
            _cancellationToken = new CancellationToken();
        }

        [Fact(DisplayName = @"Dado um handle, quando passado um command de inser��o invalido, deve lan�ar exce��o")]
        public async void Dado_Um_Handle_Quando_Passado_Um_Command_De_Insercao_Invalido_Deve_Lancar_Excecao()
        {
            var exception = await Assert.ThrowsAsync<ValidationMessage>(async () => await _handler.Handle(new InserirUsuarioCommand(), _cancellationToken));
            exception.Message.Should().Contain("Command inv�lido");
        }

        [Fact(DisplayName = @"Dado um handle, quando passado um command de atualiza��o invalido, deve lan�ar exce��o")]
        public async void Dado_Um_Handle_Quando_Passado_Um_Command_De_Atualizacao_Invalido_Deve_Lancar_Excecao()
        {
            var exception = await Assert.ThrowsAsync<ValidationMessage>(async () => await _handler.Handle(new AtualizarUsuarioCommand(), _cancellationToken));
            exception.Message.Should().Contain("Command inv�lido");
        }

        [Fact(DisplayName = @"Dado um handle, quando passado um command de exclusao invalido, deve lan�ar exce��o")]
        public async void Dado_Um_Handle_Quando_Passado_Um_Command_De_Exclusao_Invalido_Deve_Lancar_Excecao()
        {
            var exception = await Assert.ThrowsAsync<ValidationMessage>(async () => await _handler.Handle(new ExcluirUsuarioCommand(), _cancellationToken));
            exception.Message.Should().Contain("Command inv�lido");
        }
    }
}
