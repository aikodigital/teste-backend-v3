using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheatricalPlayers.Application.Handlers;
using TheatricalPlayers.Core.DataTransferObjects.Requests;

namespace TheatricalPlayers.API.Controllers
{
    [ApiController]
    [Route("api/theatricalplayers/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly StatementPrinterHandler _statementPrinterHandler;
        private readonly ILogger<StatementController> _logger;
        
        public StatementController(ILogger<StatementController> logger)
        {
            _statementPrinterHandler = new();
            _logger = logger;
            
            ArgumentNullException.ThrowIfNull(logger);
        }

        /// <summary>
        /// Generates a text statement based on the provided invoice and play information.
        /// </summary>
        /// <param name="statementRequest">The statement request containing invoice and play data.</param>
        /// <returns>A text representation of the statement.</returns>
        [HttpPost("Create/text")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateTxtStatement([FromBody] StatementRequest statementRequest)
        {
            try
            {
                var text = _statementPrinterHandler.PrintTxt(statementRequest.Invoice, statementRequest.Plays);
                return Ok(text);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating the text statement.");
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Generates an XML statement based on the provided invoice and play information.
        /// </summary>
        /// <param name="statementRequest">The statement request containing invoice and play data.</param>
        /// <returns>An XML representation of the statement.</returns>
        [HttpPost("Create/xml")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateXmlStatement([FromBody] StatementRequest statementRequest)
        {
            try
            {
                var xml = _statementPrinterHandler.PrintXml(statementRequest.Invoice, statementRequest.Plays);
                return Ok(xml);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating the XML statement.");
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Generates a JSON statement based on the provided invoice and play information.
        /// </summary>
        /// <param name="statementRequest">The statement request containing invoice and play data.</param>
        /// <returns>A JSON representation of the statement.</returns>
        [HttpPost("Create/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateJsonStatement([FromBody] StatementRequest statementRequest)
        {
            try
            {
                var json = _statementPrinterHandler.GetStatement(statementRequest.Invoice, statementRequest.Plays);
                return Ok(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating the JSON statement.");
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
