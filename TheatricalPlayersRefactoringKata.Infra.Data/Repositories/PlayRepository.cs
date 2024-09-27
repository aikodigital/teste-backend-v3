using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Infra.Data.Context;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Repositories;

public class PlayRepository : BaseRepository<Play, ApplicationDbContext>, IPlayRepository
{
    public PlayRepository(ApplicationDbContext context) : base(context)
    {

    }
}
