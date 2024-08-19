using Microsoft.EntityFrameworkCore;

using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Common;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.Play;

namespace TheatricalPlayersRefactoringKata.Server.Database.Repositories
{
    public class PlayRepository : Repository<PlayEntity>
    {
        public PlayRepository(DbContextTheatricalPlayers context) : base(context)
        {
        }

        public async Task<PlayEntity?> GetByTitle(string title)
        {
            return await _dbSet.FirstOrDefaultAsync(play => play.Name == title);
        }
    }
}