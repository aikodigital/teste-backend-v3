using TS.Domain.Entities;

namespace TS.Domain.Repositories.Performances
{
    public interface IPerformancesRepository
    {
        Task CreateAsync(Performance entity);
        Task<Performance?> GetAsync(long id);
        Task<IEnumerable<Performance>> GetAllAsync();
        Task UpdateAsync(Performance entity);
        Task DeleteAsync(long id);
    }
}