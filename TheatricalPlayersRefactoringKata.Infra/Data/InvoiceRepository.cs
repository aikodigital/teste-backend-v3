using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entity;
using TheatricalPlayersRefactoringKata.Infra.Context;

public class InvoiceRepository : IInvoicesRepository
{
    private readonly AppDbContext _context;

    public InvoiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public Invoice? GetInvoiceById(Guid id)
    {
        return _context.Invoices.FirstOrDefault(i => i.Id == id);
    }

    public Invoice? GetInvoiceById(int id)
    {
        throw new NotImplementedException("Getting invoice by int ID is not supported.");
    }

    public void CreateInvoice(Invoice invoice)
    {
        if (invoice == null) throw new ArgumentNullException(nameof(invoice));

        _context.Invoices.Add(invoice);
        _context.SaveChanges();
    }

    public void UpdateInvoice(Invoice invoice)
    {
        if (invoice == null) throw new ArgumentNullException(nameof(invoice));

        var existingInvoice = _context.Invoices.Find(invoice.Id);
        if (existingInvoice == null) 
            throw new InvalidOperationException("Invoice not found.");

        existingInvoice.Customer = invoice.Customer;
        existingInvoice.Performances = invoice.Performances;

        _context.Invoices.Update(existingInvoice);
        _context.SaveChanges();
    }

    public void DeleteInvoice(Guid id)
    {
        var invoice = _context.Invoices.Find(id);
        if (invoice == null)
            throw new InvalidOperationException("Invoice not found.");

        _context.Invoices.Remove(invoice);
        _context.SaveChanges();
    }

    public IEnumerable<Invoice> GetAllInvoices()
    {
        return _context.Invoices.ToList();
    }

    public IEnumerable<Invoice> GetInvoicesByCriteria(Func<Invoice, bool> filter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        return _context.Invoices.AsEnumerable().Where(filter).ToList();
    }
}
