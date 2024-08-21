using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Infra.DataBase.Repository
{
    public class InvoiceRepository
    {
        private readonly TheatricalContext _context;

        public InvoiceRepository(IDbContextFactory<TheatricalContext> factory)
        {
            _context = factory.CreateDbContext();
        }

        public void CreateInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();
        }

        public void UpdateInvoice(Invoice invoice)
        {
            var oldInvoice = _context.Invoices
                .Include(i => i.Performances) 
                .FirstOrDefault(x => x.Id == invoice.Id);

            if (oldInvoice != null)
            {
                oldInvoice.Customer = invoice.Customer;
                oldInvoice.Performances = invoice.Performances;

                _context.Invoices.Update(oldInvoice);
                _context.SaveChanges();
            }
        }

        public void DeleteInvoice(int id)
        {
            var invoiceToRemove = _context.Invoices
                .Include(i => i.Performances) 
                .FirstOrDefault(x => x.Id == id);

            if (invoiceToRemove != null)
            {
                _context.Invoices.Remove(invoiceToRemove);
                _context.SaveChanges();
            }
        }

        public Invoice? GetInvoiceById(int id)
        {
            return _context.Invoices
                .Include(i => i.Performances) 
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Invoice> GetAllInvoices()
        {
            return _context.Invoices
                .Include(i => i.Performances)
                .AsNoTracking()
                .ToList();
        }
    }
}
