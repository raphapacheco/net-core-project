using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using FluentAssertions;
using BackEnd.NetCore.Api.TesteIntegracao.Extensions;
using BackEnd.NetCore.Api.TesteIntegracao.Abstractions;
using BackEnd.NetCore.Api;
using BackEnd.NetCore.Api.TesteIntegracao.Fixtures;
using BackEnd.NetCore.Usuario.Queries.DataContracts;
using BackEnd.NetCore.Common.DataContracts;

namespace BackEnd.NetCore.Api.TesteIntegracao.Controllers
{
    public class UsuarioControllerTesteIntegracao : WebApplicationTesteIntegracao, IClassFixture<UsuarioFixture>
    {
        private readonly UsuarioFixture _fixture;
        const string ENDPOINT = "/usuario/";

        public UsuarioControllerTesteIntegracao(WebApplicationFactory<Startup> factory, UsuarioFixture fixture) : base(factory)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Dados os endpoints para Usuario, deve ser possível fazer o CRUD")]
        public async Task Dados_os_endpoints_Agencia_deve_ser_posível_fazer_o_CRUD_da_entidade()
        {
            await _client.Autenticar();

            var id = await Deve_persistir_dados_quando_efetuado_post();
            await Deve_consultar_dados_quando_efetuado_get(id);
            await Deve_atualizar_dados_quando_efetuado_put(id);
            await Deve_excluir_dados_quando_efetuado_delete(id);
            await Deve_consultar_dados_quando_efetuado_get_all();
        }        

        private async Task<int> Deve_persistir_dados_quando_efetuado_post()
        {
            var postResponse = await _client.PostAsync(ENDPOINT, _fixture.GerarPostUsuario());
            var postResult = postResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var postObject = JsonConvert.DeserializeObject<ResponseBase>(postResult);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getResponse = await _client.GetAsync(ENDPOINT + postObject.Id);
            var getResult = getResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            
            postResult.Should().NotBeNull();
            getResult.Should().NotBeNull();

            return postObject.Id;
        }

        private async Task Deve_consultar_dados_quando_efetuado_get(int id)
        {            
            var getResponse = await _client.GetAsync(ENDPOINT + id);
            var getResult = getResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var entidade = JsonConvert.DeserializeObject<ConsultarUsuarioQueryResponse>(getResult);

            entidade.Nome.Should().Be("User Test");
        }

        private async Task Deve_atualizar_dados_quando_efetuado_put(int id)
        {
            var getResponse = await _client.GetAsync(ENDPOINT + id);
            var getResult = getResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var entidade = JsonConvert.DeserializeObject<ConsultarUsuarioQueryResponse>(getResult);

            var putResponse = await _client.PutAsync(ENDPOINT, _fixture.GerarPutUsuario(entidade));
            var putResult = putResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            putResult.Should().NotBeNull();
        }

        private async Task Deve_excluir_dados_quando_efetuado_delete(int id)
        {           
            var deleteResponse = await _client.DeleteAsync(ENDPOINT + id);
            var deleteResult = deleteResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            deleteResult.Should().NotBeNull();
        }

        private async Task Deve_consultar_dados_quando_efetuado_get_all()
        {
            var getResponse = await _client.GetAsync(ENDPOINT + "?pagina=1&tamanho=2");
            var getResult = getResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var resultado = JsonConvert.DeserializeObject<ConsultarPaginadoUsuarioResponse>(getResult);

            resultado.Should().NotBeNull();
            resultado.Pagina.Should().Be(1);
            resultado.Quantidade.Should().Be(2);
        }
    }
}
