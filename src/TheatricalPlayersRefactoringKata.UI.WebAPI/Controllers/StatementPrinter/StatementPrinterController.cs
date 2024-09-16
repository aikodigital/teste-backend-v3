using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.Services;

namespace TheatricalPlayersRefactoringKata.UI.WebAPI.Controllers.StatementPrinter;

[ApiController]
[Route("statement-printer/print")]
public class StatementPrinterController : ControllerBase
{
    private readonly ILogger<StatementPrinterController> _logger;
    private readonly IStatementService _statementService;

    public StatementPrinterController(
        ILogger<StatementPrinterController> logger,
        IStatementService statementService
    )
    {
        _statementService = statementService;
        _logger = logger;
    }

    [HttpPost("text")]
    public IActionResult PrintText([FromBody] StatementPrinterPrintInput input)
    {
        try
        {
            var invoice = input.Invoice.ToEntity();
            var plays = input.Plays.ToDictionary(pair => pair.Key, pair => pair.Value.ToEntity());

            var statement = _statementService.Create(
                invoice: invoice,
                plays: plays
            );

            var output = _statementService.PrintText(statement);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Something went wrong. Error message: {Message}", ex.Message);
            return UnprocessableEntity();
        }
    }

    [HttpPost("xml")]
    public IActionResult PrintXml([FromBody] StatementPrinterPrintInput input)
    {
        try
        {
            var invoice = input.Invoice.ToEntity();
            var plays = input.Plays.ToDictionary(pair => pair.Key, pair => pair.Value.ToEntity());

            var statement = _statementService.Create(
                invoice: invoice,
                plays: plays
            );

            var output = _statementService.PrintXml(statement);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Something went wrong. Error message: {Message}", ex.Message);
            return UnprocessableEntity();
        }
    }
}