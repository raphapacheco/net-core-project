using System;
using System.IO;
using BackEnd.NetCore.Api.Models;
using BackEnd.NetCore.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BackEnd.NetCore.Api.TesteUnitario.Fixtures
{
    public class TokenServiceFixture : IDisposable
    {
        private IOptions<ConfiguracaoToken> _config;
        private TokenService _tokenService;

        public TokenServiceFixture()
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", false)
               .Build();

            _config = Options.Create(configuration.GetSection(ConfiguracaoToken.Secao).Get<ConfiguracaoToken>());

            _tokenService = new TokenService(_config);
        }

        public TokenService GerarTokenService()
        {
            return _tokenService;
        }

        public void Dispose()
        {
        }
    }
}
