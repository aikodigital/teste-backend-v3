using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Common;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.PerformanceHistory;

namespace TheatricalPlayersRefactoringKata.Server.Database.Repositories
{
    public class PerformanceHistoryRepository : Repository<PerformanceHistoryEntity>
    {
        public PerformanceHistoryRepository(DbContextTheatricalPlayers context) : base(context)
        {
        }
    }
}