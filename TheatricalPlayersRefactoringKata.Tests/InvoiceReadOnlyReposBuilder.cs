using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public InvoiceReadOnlyReposBuilder FilterByMonth(Invoice invoice)
    {
        _repos.Setup(repos => repos.GenerateReport(invoice.ReturnsAsync(expenses);

        return this;
    }
}
