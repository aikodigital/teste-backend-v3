using Microsoft.EntityFrameworkCore;
using TP.Domain.Entities;
using TP.Infrastructure.Data;
using TP.Infrastructure.Repositories;

public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
{
    private readonly ApplicationDbContext _context;

    public InvoiceRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public Invoice GetInvoiceById(Guid id)
    {
        return _context.Invoices.Include(i => i.Performances).FirstOrDefault(i => i.Id == id);
    }
}
