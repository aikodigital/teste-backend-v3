using Microsoft.EntityFrameworkCore;

using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Common;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.InvoiceHistory;

namespace TheatricalPlayersRefactoringKata.Server.Database.Repositories
{
    public class InvoiceHistoryRepository : Repository<InvoiceHistoryEntity>
    {
        public InvoiceHistoryRepository(DbContextTheatricalPlayers context) : base(context)
        {
        }

        public new async Task<InvoiceHistoryEntity?> GetById(int id)
        {
            return await _dbSet.Include(invoice => invoice.PerformancesHistories).FirstOrDefaultAsync(invoice => invoice.Id == id);
        }

        public async Task<IEnumerable<InvoiceHistoryEntity>?> GetByCustomer(string customer)
        {
            return await _dbSet.Include(invoice => invoice.PerformancesHistories).Where(invoice => invoice.Customer == customer).ToListAsync();
        }
    }
}