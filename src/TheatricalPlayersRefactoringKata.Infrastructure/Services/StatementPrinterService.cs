using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Factories;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services;

public class StatementPrinterService : IStatementPrinterService
{
    public string Print(StatementEntity statement, PrintFormatEnum printFormat)
    {
        var printStatementStrategy = PrintStatementStrategyFactory.CreateStrategy(printFormat);
        return printStatementStrategy.Print(statement);
    }
}