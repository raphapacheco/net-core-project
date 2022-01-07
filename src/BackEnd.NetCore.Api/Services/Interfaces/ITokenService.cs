using BackEnd.NetCore.Common.DataContracts;
using BackEnd.NetCore.Api.Models;

namespace BackEnd.NetCore.Api.Services.Interfaces
{
    public interface ITokenService
    {
        AccessToken GerarToken(UsuarioToken usuario);
        string GerarRefreshToken(UsuarioToken usuario);
        UsuarioToken ObterUsuario(string token);
        bool ValidarRefreshToken(string refreshToken, UsuarioToken usuario);
        bool RefreshTokenExpirado(string refreshToken);
    }
}
