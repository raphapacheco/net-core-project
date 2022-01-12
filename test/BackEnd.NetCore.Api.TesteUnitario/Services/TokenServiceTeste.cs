using Xunit;
using Microsoft.IdentityModel.Tokens;
using FluentAssertions;
using BackEnd.NetCore.Api.Services;
using BackEnd.NetCore.Api.TesteUnitario.Fixtures;
using BackEnd.NetCore.Common.DataContracts;

namespace BackEnd.NetCore.Api.TesteUnitario.Services
{
    public class TokenServiceTeste : IClassFixture<TokenServiceFixture>
    {
        private readonly TokenServiceFixture _tokenServiceFixture;
        private readonly TokenService _tokenService;
        private readonly UsuarioToken _usuarioOwner;
        private readonly UsuarioToken _usuarioManager;
        private const string _refreshTokenExpirado = "+ imBK1WMFdLXiBC0xq4GYiN3osREL + zArfYF5NvFdmf1dAh / yjUy2ItkRbSGQtidLtaKAXt + x0PcdTZCEMpHAm5p18XeIr9 / nRLIuIZdZ2m49CtNSXfcqLML9Xe2F7BmIH6Xuz4KuHLTra7Q7KvK95e5n9iAgjh9";

        public TokenServiceTeste(TokenServiceFixture tokenServiceFixture)
        {
            _tokenServiceFixture = tokenServiceFixture;
            _tokenService = _tokenServiceFixture.GerarTokenService();
            _usuarioOwner = new UsuarioToken { Id = "1", Login = "OWNER", Nome = "OWNER", Email = "owner@empresa.com.br", IP = "192.168.0.1" };
            _usuarioManager = new UsuarioToken { Id = "2", Login = "MANAGER", Nome = "MANAGER", Email = "manager@empresa.com.br", IP = "192.168.0.2" };
        }

        [Fact(DisplayName = "Dado o método GerarToken, deve retornar um Token válido")]
        public void Dado_Metodo_GerarToken_Deve_Retornar_Um_Token_Valido()
        {
            var token = _tokenService.GerarToken(_usuarioOwner);
            Assert.NotNull(token);
            Assert.True(token.ExpiresIn > 0);
        }

        [Fact(DisplayName = "Dado o método GerarRefreshToken, deve retornar um RefreshToken válido")]
        public void Dado_Metodo_GerarRefreshToken_Deve_Retornar_Um_RefreshToken_Valido()
        {
            var refreshToken = _tokenService.GerarRefreshToken(_usuarioOwner);
            Assert.NotNull(refreshToken);
            Assert.False(refreshToken.Equals(""));
        }

        [Fact(DisplayName = "Dado o método ObterUsuario, deve retornar um Usuario válido")]
        public void Dado_Metodo_ObterUsuario_Deve_Retornar_Um_Usuario_Valido()
        {
            var accessToken = _tokenService.GerarToken(_usuarioOwner);
            var usuario = _tokenService.ObterUsuario(accessToken.Token);
            Assert.NotNull(usuario);
            Assert.True(usuario.Nome.Equals(_usuarioOwner.Nome));
        }

        [Fact(DisplayName = "Dado o método ObterUsuario, deve retornar erro, quando token inválido")]
        public void Dado_Metodo_ObterUsuario_Deve_Retornar_Erro_Quando_token_invalido()
        {
            const string TOKEN_INVALIDO = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwQTAwMDAwMDAxIiwidW5pcXVlX25hbWUiOiJTVVBFUlZJU09SIiwiTm9tZSI6IlNVUEVSVklTT1IgR0VSQUwiLCJFbWFpbCI6InN1cGVydmlzb3JAZW1wcmVzYS5jb20uYnIiLCJJcEFkZHJlc3MiOiIxOTIuMTY4LjAuMSIsIm5iZiI6MTYzNjE0MzMwNiwiZXhwIjoxNjM2MTUwNTA2LCJpYXQiOjE2MzYxNDMzMDZ9.7G7geeJSrUH0gu7rOc-Mkm7wxh5F2c2kKsp3qUjMAQApPGC-rQ9WBi-GEXb6-3wofao8eZyxPqhYHfjfFT4w6g";
            var exception = Assert.Throws<SecurityTokenException>(() => _tokenService.ObterUsuario(TOKEN_INVALIDO));
            exception.Message.Should().Contain("Token inválido");
        }

        [Fact(DisplayName = "Dados RefreshToken e Usuario válidos, quando validados, deve retornar verdadeiro")]
        public void Dados_RefreshToken_Usuario_Validos_Quando_Validados_Deve_Retornar_Verdadeiro()
        {
            var refreshToken = _tokenService.GerarRefreshToken(_usuarioOwner);
            var resultado = _tokenService.ValidarRefreshToken(refreshToken, _usuarioOwner);
            Assert.True(resultado);
        }

        [Fact(DisplayName = "Dados RefreshToken e Usuario invalidos, quando validados, deve retornar falso")]
        public void Dados_RefreshToken_Usuario_Invalidos_Quando_Validados_Deve_Retornar_Falso()
        {
            var refreshToken = _tokenService.GerarRefreshToken(_usuarioOwner);
            var resultado = _tokenService.ValidarRefreshToken(refreshToken, _usuarioManager);
            Assert.False(resultado);
        }

        [Fact(DisplayName = "Dado um RefreshToken novo, quando verificada a expiração, deve retornar falso")]
        public void Dado_RefreshToken_Novo_Quando_Verificada_Expiracao_Deve_Retornar_Falso()
        {
            var refreshToken = _tokenService.GerarRefreshToken(_usuarioOwner);
            var resultado = _tokenService.RefreshTokenExpirado(refreshToken);
            Assert.False(resultado);
        }

        [Fact(DisplayName = "Dado um RefreshToken antigo, quando verificada a expiração, deve retornar verdadeiro")]
        public void Dado_RefreshToken_Antigo_Quando_Verificada_Expiracao_Deve_Retornar_Verdadeiro()
        {
            var resultado = _tokenService.RefreshTokenExpirado(_refreshTokenExpirado);
            Assert.True(resultado);
        }
    }
}
