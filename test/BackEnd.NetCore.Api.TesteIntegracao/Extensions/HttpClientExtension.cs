using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BackEnd.NetCore.Common.DataContracts;
using BackEnd.NetCore.Api.TesteIntegracao.Fixtures;
using Newtonsoft.Json;

namespace BackEnd.NetCore.Api.TesteIntegracao.Extensions
{
    public static class HttpClientExtension
    {
        public async static Task Autenticar(this HttpClient client)
        {            
            var authResponse = await client.PostAsync("/auth/token/", new FormUrlEncodedContent(AuthFixture.GetValidUser()));
            var resultContent = authResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var token = JsonConvert.DeserializeObject<TokenResponse>(resultContent);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.AccessToken);
        }
    }
}
