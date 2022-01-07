using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BackEnd.NetCore.Api.Models;

namespace BackEnd.NetCore.Api.Configurations
{
    public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly IOptions<ConfiguracaoToken> _configuracaoToken;

        public ConfigureJwtBearerOptions(IOptions<ConfiguracaoToken> configuracaoToken)
        {
            _configuracaoToken = configuracaoToken ?? throw new ArgumentNullException(nameof(configuracaoToken));
        }
        public void Configure(string name, JwtBearerOptions options)
        {
            Configure(options);
        }

        public void Configure(JwtBearerOptions options)
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_configuracaoToken.Value.GetSecretAsByteArray()),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
