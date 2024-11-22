using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories
{
    public class PerformanceRepository: IPerformanceRepository
    {
        private readonly ApplicationDbContext _context;

        public PerformanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Performance> AddAsync(Domain.Entities.Performance performance)
        {
            _context.Performances.Add(performance);
            await _context.SaveChangesAsync();
            return performance;
        }
    }
}
