#region

using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.API.Repositories.DTOs;
using TheatricalPlayersRefactoringKata.Core.Entities;

#endregion

namespace TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;

public interface IPerformanceRepository
{
    Task<IActionResult> CreatePerformance(PerfRequest perf, Guid playId);
    Task<IEnumerable<PerfResponse>> GetPerformances();
    Task<IActionResult> GetPerformancesById(Guid performanceId);
    Task<IActionResult> UpdatePerformance(Guid performanceId, Performance perf);
    Task<IActionResult> DeletePerformance(Guid performanceId);
}