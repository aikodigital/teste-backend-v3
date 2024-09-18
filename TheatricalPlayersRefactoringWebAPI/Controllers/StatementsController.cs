using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using TheatricalPlayersRefactoringKata.Infrastructure;
using TheatricalPlayersRefactoringWebAPI.DTO;

namespace TheatricalPlayersRefactoringWebAPI.Controllers
{
    [ApiController]
    [Route("api/statements")]
    public class StatementsController : ControllerBase
    {
        private readonly StatementPrinter _statementPrinter;
        private readonly StatementMapper _mapper;

        public StatementsController(StatementPrinter statementPrinter, StatementMapper mapper)
        {
            _statementPrinter = statementPrinter;
            _mapper = mapper;
        }

        [HttpPost("txt")] // Rota para gerar relatório em TXT
        [SwaggerRequestExample(typeof(StatementRequest), typeof(StatementRequestExample))]
        public IActionResult PrintTxt([FromBody] StatementRequest request)
        {
            try
            {
                var invoice = _mapper.MapToInvoice(request.Invoice);
                var plays = _mapper.MapToPlays(request.Plays);

                string txtOutput = _statementPrinter.PrintTxt(invoice, plays);
                return Ok(txtOutput);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("xml")] // Rota para gerar relatório em XML
        [SwaggerRequestExample(typeof(StatementRequest), typeof(StatementRequestExample))]
        public IActionResult PrintXml([FromBody] StatementRequest request)
        {
            try
            {
                var invoice = _mapper.MapToInvoice(request.Invoice);
                var plays = _mapper.MapToPlays(request.Plays);

                string xmlOutput = _statementPrinter.PrintXml(invoice, plays);
                return Ok(xmlOutput);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
