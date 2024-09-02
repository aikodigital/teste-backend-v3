using Application.Plays.Commands.CreatePlay;
using Application.Plays.Commands.DeletePlay;
using Application.Plays.Commands.UpdatePlay;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public PlayController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Play>> Play([FromBody] CreatePlayCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdatePlayCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeletePlayCommand { Id = id });

            return NoContent();
        }
    }
}
