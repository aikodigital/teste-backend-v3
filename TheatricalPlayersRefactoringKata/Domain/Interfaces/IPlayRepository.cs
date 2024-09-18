using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces;

public interface IPlayRepository
{
    Play GetPlayById(string playId);
}
