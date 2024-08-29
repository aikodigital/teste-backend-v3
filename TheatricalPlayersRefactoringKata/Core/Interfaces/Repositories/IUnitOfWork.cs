using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces.Repositories;

public interface IUnitOfWork
{
    public Task CommitAsync();

}
