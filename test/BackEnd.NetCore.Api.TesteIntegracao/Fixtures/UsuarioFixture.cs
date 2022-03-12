using BackEnd.NetCore.Usuario.Commands.DataContracts;
using BackEnd.NetCore.Usuario.Queries.DataContracts;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BackEnd.NetCore.Api.TesteIntegracao.Fixtures
{
    public class UsuarioFixture
    {
        public StringContent GerarPostUsuario()
        {
            var command = new InserirUsuarioCommand()
            {
                Nome = "User Test",
                Login = "TEST",
                Email = "test@company.com",
                Senha = "pass123",
                CPF = "333.333.333-33",
                CNPJ = "92.115.984/0001-74",
                Celular = "+55(21)93333-3333"
            };

            return new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
        }

        public StringContent GerarPutUsuario(ConsultarUsuarioResponse response)
        {
            var command = new AtualizarUsuarioCommand()
            {
                Id = response.Id,
                Nome = "User Test New Name",
                Login = response.Login,
                Email = response.Email,
                Senha = "pass321",
                CPF = response.CPF,
                CNPJ = response.CNPJ,
                Celular = response.Celular
            };

            return new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
        }

        public StringContent GerarPutUsuarioInvalido()
        {
            return new StringContent(JsonConvert.SerializeObject(new AtualizarUsuarioCommand()), Encoding.UTF8, "application/json");
        }
        
        public StringContent GerarPostUsuarioInvalido()
        {
            return new StringContent(JsonConvert.SerializeObject(new InserirUsuarioCommand()), Encoding.UTF8, "application/json");
        }
    }
}
