using BackEnd.NetCore.Common.Utils;
using BackEnd.NetCore.Usuario.Commands.DataContracts;
using BackEnd.NetCore.Usuario.Queries.DataContracts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BackEnd.NetCore.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            try
            {
                var usuario = await _mediator.Send(new ConsultarUsuarioPorIdQuery() { Id = id });

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                return Ok(usuario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }            
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] InserirUsuarioCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (ValidationException e)
            {
                return BadRequest(new ErrorMessage(e.Message, e.Errors));
            }            
        }
    }
}


