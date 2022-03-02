using BackEnd.NetCore.Common.Utils;
using BackEnd.NetCore.Usuario.Commands.DataContracts;
using BackEnd.NetCore.Usuario.Queries.DataContracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
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
            catch (ValidationMessage e)
            {
                return BadRequest(e.MensagemFormatada);
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
            catch (ValidationMessage e)
            {
                return BadRequest(e.MensagemFormatada);
            }            
        }

        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery(Name = "pagina")] int pagina, [FromQuery(Name = "tamanho")] int tamanho)
        {
            try
            {
                var usuarios = await _mediator.Send(new ConsultarPaginadoUsuarioQuery() { Pagina = pagina, Tamanho = tamanho});

                if (usuarios == null || usuarios.Itens.Count().Equals(0))
                {
                    return NotFound("Nenhum usuário encontrado");
                }

                return Ok(usuarios);
            }
            catch (ValidationMessage e)
            {
                return BadRequest(e.MensagemFormatada);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] AtualizarUsuarioCommand command)
        {
            try
            {
                var usuario = await _mediator.Send(command);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                return Ok(usuario);
            }
            catch (ValidationMessage e)
            {
                return BadRequest(e.MensagemFormatada);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var retorno = await _mediator.Send(new ExcluirUsuarioCommand() { Id = id });

                if (retorno == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                return Ok(retorno);
            }
            catch (ValidationMessage e)
            {
                return BadRequest(e.MensagemFormatada);
            }
        }
    }
}


