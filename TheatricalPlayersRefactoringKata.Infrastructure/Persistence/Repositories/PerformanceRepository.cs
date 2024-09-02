using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces.Repositories;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Persistence.Repositories
{
    public class PerformanceRepository : BaseRepository<Performance>, IPerformanceRepository
    {
        private readonly DBContext _dbContext;

        public PerformanceRepository(DBContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
