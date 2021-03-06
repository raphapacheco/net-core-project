using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BackEnd.NetCore.Common.DataContracts;
using BackEnd.NetCore.Api.TesteIntegracao.Abstractions;
using BackEnd.NetCore.Api.TesteIntegracao.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace BackEnd.NetCore.Api.TesteIntegracao.Controllers
{
    public class AutenticacaoControllerTeste : WebApplicationTesteIntegracao
    {
        private const string REFRESH_TOKEN_EXPIRADO = "+imBK1WMFdLXiBC0xq4GYnFDWAOR9IgnPpVECv5bSp6fexI8gJimdAUmvwjjSZwlBMj5uRTvBVdT0JeJFSbFvM4WWI6Aam7ptL+mkX4+IrIzQYI64gi4usBLePT/K7t9u+Tp+TyBqXlcEU8OluyizP9ZqlduEzHX30POylbIasrDs6bcpvWFSY8+e8M4tRW2";
        private const string TOKEN_EXPIRADO = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJPV05FUiIsIk5vbWUiOiJPd25lciIsIkVtYWlsIjoib3duZXJAY29tcGFueS5jb20iLCJJcEFkZHJlc3MiOiIwLjAuMC4wIiwibmJmIjoxNjQyMDk4MjIzLCJleHAiOjE2NDIwOTgyMzgsImlhdCI6MTY0MjA5ODIyM30.3bgrRPexUwEvcyKdMbKBQW_1K3jMTmkdgoR_VS38jiQ";
        
        public AutenticacaoControllerTeste(WebApplicationFactory<Startup> factory) : base(factory) 
        { }

        [Fact]
        public async Task TestePing()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/auth/ping/");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Dado o endpoint 'token', deve retornar Bad Request, quando usuário e senha inválido")]
        public async Task Dado_Endpoint_Token_Deve_Retornar_Bad_Request_Quando_Usuario_E_Senha_Invalido()
        {
            var client = _factory.CreateClient();
            var response = await client.PostAsync("/auth/token/", new FormUrlEncodedContent(AuthFixture.GetInvalidUser()));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Dado o endpoint 'token', deve retornar um Token válido")]
        public async Task Dado_Endpoint_Token_Deve_Retornar_Um_Token_Valido()
        {
            var client = _factory.CreateClient();
            var response = await client.PostAsync("/auth/token/", new FormUrlEncodedContent(AuthFixture.GetValidUser()));
            var resultContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var token = JsonConvert.DeserializeObject<TokenResponse>(resultContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.False(token.AccessToken.Equals(""));
        }

        [Fact(DisplayName = "Dado o endpoint 'refresh-token', deve retornar um Token válido")]
        public async Task Dado_Endpoint_RefresToken_Deve_Retornar_Um_Token_Valido()
        {
            var client = _factory.CreateClient();
            var response = await client.PostAsync("/auth/token/", new FormUrlEncodedContent(AuthFixture.GetValidUser()));
            var resultContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var token = JsonConvert.DeserializeObject<TokenResponse>(resultContent);
           
            var post = new Dictionary<string, string>();
            post.Add("username", "OWNER");
            post.Add("token", token.AccessToken);
            post.Add("refreshToken", token.RefreshToken);

            response = await client.PostAsync("/auth/refresh-token/", new FormUrlEncodedContent(post));
            resultContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            token = JsonConvert.DeserializeObject<TokenResponse>(resultContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.False(token.AccessToken?.Equals(""));
        }

        [Fact(DisplayName = "Dado o endpoint 'refresh-token', deve retornar bad request, quando refreshToken expirado")]
        public async Task Dado_Endpoint_RefresToken_Deve_Retornar_Bad_Request_Quando_RefreshToken_Expirado()
        {
            var client = _factory.CreateClient();
            var response = await client.PostAsync("/auth/token/", new FormUrlEncodedContent(AuthFixture.GetValidUser()));
            var resultContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var token = JsonConvert.DeserializeObject<TokenResponse>(resultContent);

            var post = new Dictionary<string, string>();
            post.Add("username", "OWNER");
            post.Add("token", token.AccessToken);
            post.Add("refreshToken", REFRESH_TOKEN_EXPIRADO);

            response = await client.PostAsync("/auth/refresh-token/", new FormUrlEncodedContent(post));
            resultContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("RefreshToken expirado", resultContent);
        }

        [Fact(DisplayName = "Dado o endpoint 'refresh-token', deve retornar bad request, quando usuário inválido")]
        public async Task Dado_Endpoint_RefresToken_Deve_Retornar_Bad_Request_Quando_Usuario_Invalido()
        {
            var client = _factory.CreateClient();
            var response = await client.PostAsync("/auth/token/", new FormUrlEncodedContent(AuthFixture.GetValidUser()));
            var resultContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var token = JsonConvert.DeserializeObject<TokenResponse>(resultContent);

            var post = new Dictionary<string, string>();
            post.Add("username", "INVALID");
            post.Add("token", token.AccessToken);
            post.Add("refreshToken", token.RefreshToken);

            response = await client.PostAsync("/auth/refresh-token/", new FormUrlEncodedContent(post));
            resultContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Usuário ou refreshToken inválidos", resultContent);
        }

        [Fact(DisplayName = "Dado o endpoint 'validate', deve retornar 'OK' quando passado um Token válido")]
        public async Task Dado_Endpoint_Validate_Deve_Retornar_OK_Quando_Passado_Um_Token_Valido()
        {
            var client = _factory.CreateClient();
            var response = await client.PostAsync("/auth/token/", new FormUrlEncodedContent(AuthFixture.GetValidUser()));
            var resultContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var  token = JsonConvert.DeserializeObject<TokenResponse>(resultContent);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.AccessToken);
            response = await client.GetAsync("/auth/validate/");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Dado o endpoint 'logout', deve retornar 'OK'")]
        public async Task Dado_Endpoint_Validate_Deve_Retornar_OK()
        {
            var client = _factory.CreateClient();
            var deleteResponse = await client.DeleteAsync("/auth/logout/");
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }

        [Fact(DisplayName = "Dado o endpoint 'validate', deve retornar 'Unauthorized' quando passado um Token expirado")]
        public async Task Dado_Endpoint_Validate_Deve_Retornar_Unauthorized_Quando_Passado_Um_Token_Expirado()
        {
            var client = _factory.CreateClient();            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", TOKEN_EXPIRADO);
            var response = await client.GetAsync("/auth/validate/");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
