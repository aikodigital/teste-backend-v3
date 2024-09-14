using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Interface
{
    public interface IUnitOfWork
    {
        Task<int> Commit();
    }
}
