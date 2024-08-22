using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Repositories
{
    public interface IPerformanceRepository
    {
        Task CreatePerformance(Performance performance);
        Task<IEnumerable<Performance>> GetPerformances();
        Task<IEnumerable<Performance>> GetPerformancesByIds(List<Guid> performanceId);
    }
}
