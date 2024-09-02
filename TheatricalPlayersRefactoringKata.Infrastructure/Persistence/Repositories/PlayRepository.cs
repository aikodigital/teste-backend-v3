using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces.Repositories;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Persistence.Repositories
{
    public class PlayRepository : BaseRepository<Play>, IPlayRepository
    {
        private readonly DBContext _dbContext;

        public PlayRepository(DBContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Play> GetByNameAsync(string name) => await _dbSet.FirstOrDefaultAsync(p => p.Name == name);
    }
}
