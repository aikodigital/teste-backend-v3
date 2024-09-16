using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services;

public interface IStatementPrinterService
{
    string Print(StatementEntity statement, PrintFormatEnum printFormat);
}