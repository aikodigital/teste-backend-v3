#region

using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.API.Repositories.DTOs;

#endregion

namespace TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;

public interface IPerformanceRepository
{
    Task<IActionResult> CreatePerformance(PerfRequest perf, Guid playId);
    Task<IEnumerable<PerfResponse>> GetPerformances();
    Task<IActionResult> GetPerformancesById(Guid performanceId);
    Task<IActionResult> DeletePerformance(Guid performanceId);
}