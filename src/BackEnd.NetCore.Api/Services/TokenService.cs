using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BackEnd.NetCore.Common.DataContracts;
using BackEnd.NetCore.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using BackEnd.NetCore.Api.Models;
using System.Linq;
using System.Text.Json;
using BackEnd.NetCore.Common.Utils;

namespace BackEnd.NetCore.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<ConfiguracaoToken> _configuration;

        public TokenService(IOptions<ConfiguracaoToken> configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public AccessToken GerarToken(UsuarioToken usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                     {
                          new Claim(ClaimTypes.NameIdentifier, usuario.Identificador),
                          new Claim(ClaimTypes.Name, usuario.Login),
                          new Claim("Nome", usuario.Nome),
                          new Claim("Email", usuario.Email),
                          new Claim("IpAddress", usuario.IP)
                     }),
                Expires = DateTime.UtcNow.AddHours(_configuration.Value.ExpiresIn),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Secret.GetSecretAsByteArray()), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            DateTimeOffset expiresIn = tokenDescriptor.Expires.Value;

            return new AccessToken { Token = tokenHandler.WriteToken(token), ExpiresIn = int.Parse(expiresIn.ToUnixTimeSeconds().ToString()) };
        }

        public UsuarioToken ObterUsuario(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Secret.GetSecretAsByteArray()),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            };
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token inválido");

            return new UsuarioToken()
            {
                Identificador = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                Login = principal.Identity.Name,
                Nome = principal.Claims.FirstOrDefault(x => x.Type == "Nome")?.Value,
                Email = principal.Claims.FirstOrDefault(x => x.Type == "Email")?.Value,
                IP = principal.Claims.FirstOrDefault(x => x.Type == "IpAddress")?.Value
            };
        }

        public string GerarRefreshToken(UsuarioToken usuario)
        {
            var informacoesRefreshToken = new RefreshTokenInf
            {
                Identificador = usuario.Identificador,
                Nome = usuario.Nome,
                Login = usuario.Login,
                Email = usuario.Email,
                IP = usuario.IP,
                DataCriacao = DateTime.UtcNow
            };

            return TripleDes.Encrypt(Secret.GetSecretAsByteArray(), JsonSerializer.Serialize(informacoesRefreshToken));
        }

        public bool ValidarRefreshToken(string refreshToken, UsuarioToken usuario)
        {
            RefreshTokenInf informacoesRefreshToken = JsonSerializer.Deserialize<RefreshTokenInf>(TripleDes.Decrypt(Secret.GetSecretAsByteArray(), refreshToken));

            return informacoesRefreshToken.Identificador.Equals(usuario.Identificador) &&
                   informacoesRefreshToken.Nome.ToUpper().Equals(usuario.Nome.ToUpper()) &&
                   informacoesRefreshToken.IP.ToUpper().Equals(usuario.IP.ToUpper());
        }

        public bool RefreshTokenExpirado(string refreshToken)
        {
            RefreshTokenInf informacoesRefreshToken = JsonSerializer.Deserialize<RefreshTokenInf>(TripleDes.Decrypt(Secret.GetSecretAsByteArray(), refreshToken));
            return informacoesRefreshToken.DataCriacao.AddHours(_configuration.Value.ExpiresIn * 2) < DateTime.UtcNow;
        }
    }
}
