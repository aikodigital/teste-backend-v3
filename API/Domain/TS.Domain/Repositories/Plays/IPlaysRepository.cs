using TS.Domain.Entities;

namespace TS.Domain.Repositories.Plays
{
    public interface IPlaysRepository
    {
        Task CreateAsync(Play entity);
        Task<Play?> GetAsync(long id);
        Task<IEnumerable<Play>> GetAllAsync();
        Task UpdateAsync(Play entity);
        Task DeleteAsync(long id);
    }
}