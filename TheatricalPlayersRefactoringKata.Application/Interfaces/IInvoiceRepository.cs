namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IInvoicesRepository
    {
        Domain.Entity.Invoice? GetInvoiceById(Guid id);
        void CreateInvoice(Domain.Entity.Invoice invoice);
        void UpdateInvoice(Domain.Entity.Invoice invoice);
        void DeleteInvoice(Guid id);
        IEnumerable<Domain.Entity.Invoice> GetAllInvoices();
        IEnumerable<Domain.Entity.Invoice> GetInvoicesByCriteria(Func<Domain.Entity.Invoice, bool> filter);
    }
}