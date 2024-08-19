using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Repositories
{
    public interface IPlayRepository
    {
        Task CreatePlay(Play play);
        Task<IEnumerable<Play>> GetPlays();
    }
}
