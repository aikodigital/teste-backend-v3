using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Repositories.Plays
{
    public interface IPlaysWriteOnlyRepository
    {
        Task Add(Play play);
    }
}
