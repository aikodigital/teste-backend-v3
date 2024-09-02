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
    }
}
