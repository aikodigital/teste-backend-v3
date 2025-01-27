using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infra.Interfaces.Repository
{
    public interface IPerformanceRepository
    {
        Task<PerformanceEntity> AddPerformance(PerformanceEntity performance);
        Task<IEnumerable<PerformanceEntity>> ListPerformances();
        Task<bool> DeletePerformance(long id);
        Task<IEnumerable<PerformanceEntity>> GetPerformance(long id);
        Task<PerformanceEntity> UpdatePerformance(PerformanceEntity performance);


    }
}