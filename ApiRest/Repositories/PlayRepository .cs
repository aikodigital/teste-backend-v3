using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;
using TheatricalPlayersRefactoringKata.infra;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.API.Repositories
{
    public class PlayRepository : GenericRepository<Play>, IPlayRepository
    {
        public PlayRepository(ApiDbContext context) : base(context) { }

        public async Task<Play> GetByNameAsync(string name)
        {
            return await _context.Set<Play>().FirstOrDefaultAsync(p => p.Name == name);
        }

    }
}
