using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces.Services;

public interface IStatementPrinterService
{
    Task<string> Print(Invoice invoice);
    Task<int> Calculate(Play play, Performance perf, TypeGenre genre, int lines);
    Task AddStatementCustomer(Invoice invoice);
}
