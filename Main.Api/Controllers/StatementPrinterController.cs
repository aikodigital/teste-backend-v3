using Main.Application.Services.StatementPrinter;
using Main.Contracts.StatementPrinter;
using Microsoft.AspNetCore.Mvc;

namespace Main.Api.Controllers
{
    [ApiController]
    [Route("statementPrinter")]
    public class StatementPrinterController : ControllerBase
    {
        private readonly Func<string, IStatementPrinterService> _statementPrinterServiceFactory;

        public StatementPrinterController(Func<string, IStatementPrinterService> statementPrinterServiceFactory)
        {
            _statementPrinterServiceFactory = statementPrinterServiceFactory;
        }

        [HttpPost("print")]
        public IActionResult Print(StatementPrinterRequest request)
        {
            var statementPrinterService = _statementPrinterServiceFactory("default");
            var result = statementPrinterService.Print(request.invoice,request.plays);
            var response = new StatementPrinterResponse(result.Result);
            return Ok(response);
        }

        [HttpPost("xml")]
        public IActionResult Xml(StatementPrinterRequest request)
        {
            var statementPrinterService = _statementPrinterServiceFactory("xml");
            var result = statementPrinterService.Print(request.invoice, request.plays);
            var response = new StatementPrinterResponse(result.Result);
            return Ok(response);
        }
    }
}
