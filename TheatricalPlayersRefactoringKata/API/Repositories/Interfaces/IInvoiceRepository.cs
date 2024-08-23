#region

using TheatricalPlayersRefactoringKata.Core.Entities;

#endregion

namespace TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;

public interface IInvoiceRepository
{
    Task CreateInvoice(Invoice invoice);
    Task<Invoice> GetInvoiceById(Guid invoiceId);
    Task<IEnumerable<Invoice>> GetInvoices();
}