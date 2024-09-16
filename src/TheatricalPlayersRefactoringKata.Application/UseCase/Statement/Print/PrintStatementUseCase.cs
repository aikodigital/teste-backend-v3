using TheatricalPlayersRefactoringKata.Application.Services;

namespace TheatricalPlayersRefactoringKata.Application.UseCase.Statement.Print;

public class PrintStatementUseCase : IPrintStatementUseCase
{
    private readonly ICreateStatementUseCase _createStatementUseCase;
    private readonly IStatementPrinterService _statementPrinterService;

    public PrintStatementUseCase(ICreateStatementUseCase createStatementUseCase, IStatementPrinterService statementPrinterService)
    {
        _createStatementUseCase = createStatementUseCase;
        _statementPrinterService = statementPrinterService;
    }

    public string Execute(PrintStatementInput input)
    {
        var createStatementOutput = _createStatementUseCase.Execute(input.ToCreateStatementInput());
        return _statementPrinterService.Print(
            statement: createStatementOutput.Statement,
            printFormat: input.PrintFormat
        );
    }
}