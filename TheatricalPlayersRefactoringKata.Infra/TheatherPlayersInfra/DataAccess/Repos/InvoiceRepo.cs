using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repos;

namespace TheatherPlayersInfra.DataAccess.Repos;

internal class InvoiceRepo : IInvoice, IInvoicesReadOnlyRepository
{
    private readonly TheatherPlayersDbContext _dbContext;
    public InvoiceRepo(TheatherPlayersDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Invoice invoice)
    {
        await _dbContext.Invoices.AddAsync(invoice);
    }
    public async Task<List<Invoice>> GetAll()
    {
        return await _dbContext.Invoices.AsNoTracking().ToListAsync();

    }

    public async Task<Invoice?> GetByCustomer(string name)
    {
        return await _dbContext.Invoices
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Customer == name);
    }
    public async Task<List<Invoice>> GenerateReport(Invoice invoice, string customerName)
    {
        return await _dbContext.Invoices.AsNoTracking().Where(invoice => invoice.Customer == customerName).ToListAsync();
    }
}

