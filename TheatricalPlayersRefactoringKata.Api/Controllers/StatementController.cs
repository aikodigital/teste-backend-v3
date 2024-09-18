using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheatricalPlayersRefactoringKata.Application.Services.Statement;

namespace TheatricalPlayersRefactoringKata.Api.Controllers
{
    [Route("api/statement")]
    [Produces("application/json")]
    [ApiController]
    public class StatementController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public StatementController(
            IServiceProvider serviceProvider
        )
        {
            _serviceProvider = serviceProvider;
        }

        [HttpPost("generate/txt")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenereteStatementTxt([FromBody] Domain.Entities.Invoice invoice)
        {
            var strategy = _serviceProvider.GetService<TextStatementGenerator>();
            var result = await new StatementService(strategy).Execute(invoice);

            return Ok(result);
        }

        [HttpPost("generate/xml")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenereteStatementXml([FromBody] Domain.Entities.Invoice invoice)
        {
            var strategy = _serviceProvider.GetService<XmlStatementGenerator>();
            var result = await new StatementService(strategy).Execute(invoice);

            return Ok(result);
        }
    }
}
