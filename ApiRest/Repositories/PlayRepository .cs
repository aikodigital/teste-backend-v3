using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;
using TheatricalPlayersRefactoringKata.infra;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.API.Repositories
{
    public class PlayRepository : GenericRepository<Play>, IPlayRepository
    {
        public PlayRepository(ApiDbContext context) : base(context) { }

    }
}
