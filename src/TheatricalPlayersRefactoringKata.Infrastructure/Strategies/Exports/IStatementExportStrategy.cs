using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Strategies.Exports;

public interface IStatementExportStrategy
{
    string GenerateStatement(Invoice invoice);
    Task ExportAsync(Invoice invoice, string directory);
}