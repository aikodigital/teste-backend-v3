using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.Services;

namespace TheatricalPlayersRefactoringKata.UI.WebAPI.Controllers.StatementPrinter;

[ApiController]
[Route("statement-printer")]
public class StatementPrinterController : ControllerBase
{
    private readonly ILogger<StatementPrinterController> _logger;
    private readonly IStatementPrinterService _statementPrinterService;

    public StatementPrinterController(
        ILogger<StatementPrinterController> logger,
        IStatementPrinterService statementPrinterService
    )
    {
        _statementPrinterService = statementPrinterService;
        _logger = logger;
    }

    [HttpPost("print")]
    public IActionResult Print([FromBody] StatementPrinterPrintInput input)
    {
        try
        {
            var invoice = input.Invoice.ToEntity();
            var plays = input.Plays.ToDictionary(pair => pair.Key, pair => pair.Value.ToEntity());
            
            var result = _statementPrinterService.Print(
                invoice: invoice, 
                plays: plays
            );
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Something went wrong. Error message: {Message}", ex.Message);
            return UnprocessableEntity();
        }
    }
}