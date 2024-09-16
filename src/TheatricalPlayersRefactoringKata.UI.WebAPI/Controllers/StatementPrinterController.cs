using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.UseCase.Statement;
using TheatricalPlayersRefactoringKata.Application.UseCase.Statement.Print;

namespace TheatricalPlayersRefactoringKata.UI.WebAPI.Controllers;

[ApiController]
[Route("statement-printer")]
public class StatementPrinterController : ControllerBase
{
    private readonly ILogger<StatementPrinterController> _logger;
    private readonly IPrintStatementUseCase _printStatementUseCase;

    public StatementPrinterController(
        ILogger<StatementPrinterController> logger,
        IPrintStatementUseCase printStatementUseCase
    )
    {
        _logger = logger;
        _printStatementUseCase = printStatementUseCase;
    }

    [HttpPost("print")]
    public IActionResult Print(PrintStatementInput input)
    {
        try
        {
            var result = _printStatementUseCase.Execute(input);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Something went wrong. Error message: {Message}", ex.Message);
            return UnprocessableEntity();
        }
    }
}