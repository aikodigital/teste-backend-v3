using TheatricalPlayersRefactoringKata.Domain.Interface.Repository;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;
using TheatricalPlayersRefactoringKata.Repository.Configuration.Context;

namespace TheatricalPlayersRefactoringKata.Repository
{
    public class PerformanceRepository : Repository<Performance>, IPerformanceRepository
    {
        public PerformanceRepository(ITheatricalContext theatricalContext) : base(theatricalContext)
        {

        }
    }
}