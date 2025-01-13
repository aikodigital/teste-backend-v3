using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.StatementManipulate
{
    public interface IStatementPrinterService
    {
        Task<string> PrintAsync(Invoice invoice, Dictionary<string, Play> plays, string formatFile);
    }
}
