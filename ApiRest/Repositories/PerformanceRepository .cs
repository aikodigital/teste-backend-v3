using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;
using TheatricalPlayersRefactoringKata.API.infra;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.API.Repositories
{
    public class PerformanceRepository : GenericRepository<Performance>, IPerformanceRepository
    {
        public PerformanceRepository(ApiDbContext context) : base(context) { }

    }
}
