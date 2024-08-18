using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Repos;
public interface IInvoicesReadOnlyRepository
{
    Task<List<Invoice>> GenerateReport(Invoice invoice);
}