using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;
using TheatricalPlayersRefactoringKata.infra;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.API.Repositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApiDbContext context) : base(context) { }

        public async Task<Invoice> GetByCustomerAsync(string customer)
        {
            return await _context.Set<Invoice>().Include(i => i.Performances)
                .ThenInclude(p => p.Play)
                .FirstOrDefaultAsync(i => i.Customer == customer);
        }

    }

}
