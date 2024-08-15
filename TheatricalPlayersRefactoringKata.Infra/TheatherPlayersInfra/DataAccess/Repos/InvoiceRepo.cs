using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task Delete(Invoice expense)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Invoice expense)
    {
        throw new NotImplementedException();
    }

    public async Task<Invoice?> GetById(long id)
    {
        return await _dbContext.Invoices
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

}
