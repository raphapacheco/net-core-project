using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using BackEnd.NetCore.Common.DataContracts;
using BackEnd.NetCore.Api.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using BackEnd.NetCore.Common.Utils;
using BackEnd.NetCore.Api.Models;
using Microsoft.Extensions.Options;
using MediatR;
using BackEnd.NetCore.Usuario.Queries.DataContracts;

namespace BackEnd.NetCore.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<ConfiguracaoToken> _configurationToken;
        private readonly ITokenService _tokenService;
        private readonly IMediator _mediator;

        public AutenticacaoController(IConfiguration configuration, IOptions<ConfiguracaoToken> configurationToken, ITokenService tokenService, IMediator mediator)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _configurationToken = configurationToken ?? throw new ArgumentNullException(nameof(configurationToken));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("ping")]
        [AllowAnonymous]
        public string Ping([FromQuery] string request)
        {            
            return "pong " + TripleDes.Encrypt(_configurationToken.Value.GetSecretAsByteArray(), request); ;
        }
        
        /// <summary>Requisição do Token de acesso</summary>
        /// <remarks>Neste endpoint é possível solicitar o token de acesso.</remarks>        
        /// <param name="request"></param>                
        /// <returns></returns>
        /// <response code="200">{ "accessToken", "refreshToken", "expiresIn", "tokenType" }</response>
        /// <response code="400">{ "mensagem": "Usuário ou senha inválidos" }</response>
        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Autenticar([FromForm] AutenticacaoRequest request)
        {
            try
            {
                var usuario = await _mediator.Send(new ConsultarUsuarioQuery() { Login = request.Nome.ToUpper() });                
               
                if (string.IsNullOrEmpty(usuario?.Senha) || !request.Senha.Equals(usuario.Senha))
                    return BadRequest(new { mensagem = "Usuário ou senha inválidos" });

                var usuarioToken = new UsuarioToken()
                {
                    Identificador = usuario.Id.ToString(),
                    Nome = usuario.Nome,
                    Login = usuario.Login,
                    Email = usuario.Email,
                    IP = GetRequestIP()
                };

                var accessToken = _tokenService.GerarToken(usuarioToken);
                var refreshToken = _tokenService.GerarRefreshToken(usuarioToken);

               var token = new TokenResponse()
               {
                   AccessToken = accessToken.Token,
                   ExpiresIn = accessToken.ExpiresIn,
                   RefreshToken = refreshToken
               };

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("validate")]
        [Authorize]
        public IActionResult ValidarToken()
        {
            return Ok(new { mensagem = "Token válido" });
        }

        [HttpDelete]
        [Route("logout")]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            return Ok(new { mensagem = "Usuario desconectado!" });
        }

        [HttpPost]
        [Route("refresh-token")]
        [AllowAnonymous]
        public ActionResult<dynamic> Atualizar([FromForm] RefreshTokenRequest request)
        {
            try
            {
                var usuario = _tokenService.ObterUsuario(request.Token);

                if (!usuario.Nome.ToUpper().Equals(request.Nome.ToUpper()) || !_tokenService.ValidarRefreshToken(request.RefreshToken, usuario))
                {
                    return BadRequest(new { mensagem = "Usuário ou refreshToken inválidos" });
                }

                if (_tokenService.RefreshTokenExpirado(request.RefreshToken))
                {
                    return BadRequest(new { mensagem = "RefreshToken expirado" });
                }

                var accessToken = _tokenService.GerarToken(usuario);
                var refreshToken = _tokenService.GerarRefreshToken(usuario);

                var token = new TokenResponse()
                {
                    AccessToken = accessToken.Token,
                    ExpiresIn = accessToken.ExpiresIn,
                    RefreshToken = refreshToken
                };

                return Ok(token);         
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public string GetRequestIP()
        {
            string ip = null;

            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                ip = Request.Headers["X-Forwarded-For"];

            if (string.IsNullOrWhiteSpace(ip) && Request.HttpContext?.Connection?.RemoteIpAddress != null)
                ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            if (string.IsNullOrWhiteSpace(ip))
                ip = GetHeaderValueAs<string>("REMOTE_ADDR");

            if (string.IsNullOrWhiteSpace(ip))
                ip = "0.0.0.0";

            return ip;
        }

        public T GetHeaderValueAs<T>(string headerName)
        {
            StringValues values;

            if (Request.HttpContext?.Request?.Headers?.TryGetValue(headerName, out values) ?? false)
            {
                string rawValues = values.ToString();

                if (!string.IsNullOrWhiteSpace(rawValues))
                    return (T)Convert.ChangeType(values.ToString(), typeof(T));
            }
            return default(T);
        }
    }
}
