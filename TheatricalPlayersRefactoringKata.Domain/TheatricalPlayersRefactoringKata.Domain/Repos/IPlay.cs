using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Repos;
public interface IPlay
{
    Task Add(Play play);
    Task<List<Play>> GetAllPlays();
    Task<Play?> GetByPlay(string name);
}
