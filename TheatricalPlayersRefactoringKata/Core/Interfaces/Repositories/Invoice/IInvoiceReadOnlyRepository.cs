using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces.Repositories.Invoice;

public interface IInvoiceReadOnlyRepository
{
    Task<List<Entities.Invoice>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, string invoiceType);
    Task<IEnumerable<Entities.Invoice>> GetInvoicesAsync();

}


