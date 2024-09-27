using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Infra.Data.Context;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Repositories;

public class PerformanceRepository : BaseRepository<Performance, ApplicationDbContext>, IPerfomanceRepository
{
    public PerformanceRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}
