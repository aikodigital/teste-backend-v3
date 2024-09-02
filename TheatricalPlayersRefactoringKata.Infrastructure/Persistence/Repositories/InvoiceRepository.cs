using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces.Repositories;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Persistence.Repositories
{
    public class InvoiceRepository: BaseRepository<Invoice>, IInvoiceRepository
    {
        private readonly DBContext _dbContext;

        public InvoiceRepository(DBContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Invoice> GetByCustomerNameAsync(string costumer)
        {
            return await _dbSet
                .Include(i => i.Performances)
                .ThenInclude(p => p.Play)
                .FirstOrDefaultAsync(i => i.Customer == costumer);
        }
    }
}
