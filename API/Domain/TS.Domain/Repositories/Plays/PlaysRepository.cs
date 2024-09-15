using Microsoft.EntityFrameworkCore;
using TS.Domain.Entities;
using TS.Domain.EntityFramework;

namespace TS.Domain.Repositories.Plays
{
    public class PlaysRepository(AppDbContext context) : IPlaysRepository
    {
        private readonly AppDbContext _context = context;

        public async Task CreateAsync(Play entity)
        {
            _context.Plays.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var plays = await GetAsync(id);

            if (plays != null)
            {
                _context.Plays.Remove(plays);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Play?> GetAsync(long id)
        {
            var plays = await _context.Plays.FirstOrDefaultAsync(x => x.Id == id);
            return plays;
        }

        public async Task<IEnumerable<Play>> GetAllAsync()
        {
            var plays = await _context.Plays.ToListAsync();
            return plays;
        }

        public async Task UpdateAsync(Play entity)
        {
            _context.Plays.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}