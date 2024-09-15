using Microsoft.EntityFrameworkCore;
using TS.Domain.Entities;
using TS.Domain.EntityFramework;

namespace TS.Domain.Repositories.Performances
{
    public class PerformancesRepository(AppDbContext context) : IPerformancesRepository
    {
        private readonly AppDbContext _context = context;

        public async Task CreateAsync(Performance entity)
        {
            _context.Performances.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var performances = await GetAsync(id);

            if (performances != null)
            {
                _context.Performances.Remove(performances);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Performance?> GetAsync(long id)
        {
            var performances = await _context.Performances.FirstOrDefaultAsync(x => x.Id == id);
            return performances;
        }

        public async Task<IEnumerable<Performance>> GetAllAsync()
        {
            var performances = await _context.Performances.ToListAsync();
            return performances;
        }

        public async Task UpdateAsync(Performance entity)
        {
            _context.Performances.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}