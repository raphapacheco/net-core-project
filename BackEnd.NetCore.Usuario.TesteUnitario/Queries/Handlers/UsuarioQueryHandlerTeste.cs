using BackEnd.NetCore.Usuario.Commons.Contexts;
using FluentValidation;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;
using Microsoft.EntityFrameworkCore;
using BackEnd.NetCore.Usuario.Queries.Handlers;
using BackEnd.NetCore.Usuario.Queries.DataContracts;

namespace BackEnd.NetCore.Usuario.TesteUnitario.Queries.Handlers
{
    public class UsuarioQueryHandlerTeste
    {
        private UsuarioQueryHandler _handler;
        private readonly CancellationToken _cancellationToken;

        public UsuarioQueryHandlerTeste()
        {
            var options = new Mock<DbContextOptions<UsuarioContext>>()
            {
                CallBase = true
            };

            _handler = new UsuarioQueryHandler(new UsuarioContext(options.Object));
            _cancellationToken = new CancellationToken();
        }

        [Fact(DisplayName = @"Dado um handle, quando passado uma query de consulta por id invalida, deve lançar exceção.")]
        public async void Dado_Um_Handle_Quando_Passado_Uma_Query_De_Consulta_Por_Id_Invalida_Deve_Lancar_Excecao()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _handler.Handle(new ConsultarUsuarioPorIdQuery(), _cancellationToken));
            exception.Message.Should().Contain("Query inválida");
        }

        [Fact(DisplayName = @"Dado um handle, quando passado uma query de consulta por login invalida, deve lançar exceção.")]
        public async void Dado_Um_Handle_Quando_Passado_Uma_Query_De_Consulta_Por_Login_Invalida_Deve_Lancar_Excecao()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _handler.Handle(new ConsultarUsuarioPorLoginQuery(), _cancellationToken));
            exception.Message.Should().Contain("Query inválida");
        }

        [Fact(DisplayName = @"Dado um handle, quando passado uma query de consulta paginada invalida, deve lançar exceção.")]
        public async void Dado_Um_Handle_Quando_Passado_Uma_Query_De_Consulta_Paginada_Invalida_Deve_Lancar_Excecao()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _handler.Handle(new ConsultarPaginadoUsuarioQuery(), _cancellationToken));
            exception.Message.Should().Contain("Query inválida");
        }
    }
}
