using TheatricalPlayersRefactoringKata.Application.DTOs;
using TheatricalPlayersRefactoringKata.Application.DTOs.PerformanceDTOs;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IPerformanceService
    {
        Task<ServiceResponse<PerformanceResponse>> CreatePerformance(PerformanceRequest performanceRequest);
        Task<ServiceResponse<IEnumerable<PerformanceResponse>>> GetPerformances();
    }
}
