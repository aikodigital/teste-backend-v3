using Moq;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repos;

namespace TheatricalPlayersRefactoringKata.Tests;
public class InvoiceReadOnlyReposBuilder
{
    private readonly Mock<IInvoicesReadOnlyRepository> _repos;

    public InvoiceReadOnlyReposBuilder()
    {
        _repos = new Mock<IInvoicesReadOnlyRepository>();
    }

    public InvoiceReadOnlyReposBuilder GenerateReport(Invoice invoice, string customerName)
    {
        _repos.Setup(repos => repos.GenerateReport(invoice.ReturnsAsync(expenses);

        return this;
    }
}
