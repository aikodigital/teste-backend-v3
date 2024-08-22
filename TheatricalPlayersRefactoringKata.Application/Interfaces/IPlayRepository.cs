

namespace TheatricalPlayersRefactoringKata.Application.Interfaces;

public interface IPlayRepository
{
    Domain.Entity.Play GetPlayById(Guid playId);
    void CreatePlay(Domain.Entity.Play play);
}