using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repos;

namespace TheatherPlayersInfra.DataAccess.Repos;

internal class InvoiceRepo : IInvoice
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

}

