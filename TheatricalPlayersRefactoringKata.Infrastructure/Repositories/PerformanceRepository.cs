using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repositories;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories
{
    public class PerformanceRepository : IPerformanceRepository
    {
        private readonly AppDbContext _context;

        public PerformanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreatePerformance(Performance performance)
        {
            _context.Performances.Add(performance);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Performance>> GetPerformances()
        {
            return await _context.Performances
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
