using BackEnd.NetCore.Api.Models;
using BackEnd.NetCore.Usuario.Commands.DataContracts;
using BackEnd.NetCore.Usuario.Queries.DataContracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
            return Ok(await _mediator.Send(new ConsultarUsuarioPorIdQuery() { Id = id }));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] InserirUsuarioCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}


