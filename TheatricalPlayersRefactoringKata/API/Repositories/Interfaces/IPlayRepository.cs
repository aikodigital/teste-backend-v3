using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;

public interface IPlayRepository
{
    Task CreatePlay(Play play);
    Task<IEnumerable<Play>> GetPlays();
    Task<Play> GetPlayById(Guid playId);
}