using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersRefactoringKataAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly StatementPrinter _statementPrinter;

        public StatementController(StatementPrinter statementPrinter)
        {
            _statementPrinter = statementPrinter;
        }

        [HttpPost]
        [Route("print")]
        public IActionResult Print([FromBody] InvoiceRequest request)
        {
            var result = _statementPrinter.Print(request.Invoice, request.Plays, request.OutputType);
            return Ok(result);
        }
    }

    public class InvoiceRequest
    {
        public Invoice? Invoice { get; set; }
        public Dictionary<string, Play>? Plays { get; set; }
        public OutputType OutputType { get; set; }
    }
}
