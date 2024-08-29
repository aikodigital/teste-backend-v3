using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces.Repositories.Invoice;

public interface IInvoiceWriteOnlyRepository
{
    Task AddAsync(Entities.Invoice invoice);

}
