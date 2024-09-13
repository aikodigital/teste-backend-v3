using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services;

public interface IStatementPrinterService
{
    string Print(InvoiceEntity invoice, Dictionary<string, PlayEntity> plays);
}