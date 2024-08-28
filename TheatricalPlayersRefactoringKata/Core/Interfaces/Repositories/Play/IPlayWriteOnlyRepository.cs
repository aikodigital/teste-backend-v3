using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces.Repositories.Play;

public interface IPlayWriteOnlyRepository
{
    Task AddPlayAsync(Entities.Play play);

}
