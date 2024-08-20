using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Repositories
{
    public interface IInvoiceRepository
    {
        Task CreateInvoice(Invoice invoice);
        Task<Invoice> GetInvoiceById(Guid invoiceId);
        Task<IEnumerable<Invoice>> GetInvoices();

    }
}