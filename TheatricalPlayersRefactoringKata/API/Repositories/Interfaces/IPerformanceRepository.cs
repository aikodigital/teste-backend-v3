using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;

public interface IPerformanceRepository
{
    Task CreatePerformance(Performance performance);
    Task<IEnumerable<Performance>> GetPerformances();
    Task<IEnumerable<Performance>> GetPerformancesByIds(List<Guid> performanceId);
}