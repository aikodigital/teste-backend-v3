using Aplication.DTO;

namespace Aplication.Services.Interfaces
{
    public interface IPerformanceService
    {
        List<PerformanceDto> GetPerformances();
    }
}
