using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace BackEnd.NetCore.Api.TesteIntegracao.Abstractions
{
    public abstract class WebApplicationTesteIntegracao : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string APPSETTINGS = "appsettings.Test.json";

        protected readonly WebApplicationFactory<Startup> _factory;
        protected readonly HttpClient _client;
        public WebApplicationTesteIntegracao(WebApplicationFactory<Startup> factory)
        {
            var configPath = Directory.GetCurrentDirectory() + "/" + APPSETTINGS;

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(configPath);
                });
            });

            _client = _factory.CreateClient();
        }
    }
}
