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
using BackEnd.NetCore.Common.Extensions;

namespace BackEnd.NetCore.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IMediator _mediator;

        public AutenticacaoController(ITokenService tokenService, IMediator mediator)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("ping")]
        [AllowAnonymous]
        public string Ping()
        {            
            return "pong ";
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
            var usuario = await _mediator.Send(new ConsultarUsuarioPorLoginQuery() { Login = request.Nome.ToUpper() });
            var senhaCriptografada = TripleDes.Encrypt(Secret.GetSecretAsByteArray(), request.Senha);

            if (string.IsNullOrEmpty(usuario?.Senha) || !senhaCriptografada.Equals(usuario.Senha))
                return BadRequest(new { mensagem = "Usuário ou senha inválidos" });

            var usuarioToken = new UsuarioToken()
            {
                Id = usuario.Id.ToString(),
                Nome = usuario.Nome,
                Login = usuario.Login,
                Email = usuario.Email,
                IP = Request.GetRequestIP()
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
    }
}
