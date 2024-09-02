using Application.Performances.Commands.CreatePerformance;
using Application.Plays.Commands.UpdatePlay;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PerformanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreatePerformanceCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
