using Main.Application.Services.StatementPrinter;
using Main.Contracts.StatementPrinter;
using Microsoft.AspNetCore.Mvc;

namespace Main.Api.Controllers
{
    [ApiController]
    [Route("statementPrinter")]
    public class StatementPrinterController : ControllerBase
    {
        private readonly IStatementPrinterService _statementPrinterService;

        public StatementPrinterController(IStatementPrinterService statementPrinterService)
        {
            _statementPrinterService = statementPrinterService;
        }

        [HttpPost("print")]
        public IActionResult Register(StatementPrinterRequest request)
        {
            var result = _statementPrinterService.Print(request.invoice,request.plays);
            var response = new StatementPrinterResponse(result.Result);
            return Ok(response);
        }
    }
}
