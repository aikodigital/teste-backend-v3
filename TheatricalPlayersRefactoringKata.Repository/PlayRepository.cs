using TheatricalPlayersRefactoringKata.Domain.Interface.Repository;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;
using TheatricalPlayersRefactoringKata.Repository.Configuration.Context;

namespace TheatricalPlayersRefactoringKata.Repository
{
    public class PlayRepository : Repository<Play>, IPlayRepository
    {
        public PlayRepository(ITheatricalContext theatricalContext) : base(theatricalContext)
        {

        }
    }
}