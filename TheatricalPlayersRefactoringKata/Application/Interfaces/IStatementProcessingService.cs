using System.Threading.Tasks;
using System.Threading;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces;

public interface IStatementProcessingService
{
    void EnqueueInvoice(Invoice invoice);
    Task ProcessInvoiceAsync(Invoice invoice, CancellationToken cancellationToken);
}
