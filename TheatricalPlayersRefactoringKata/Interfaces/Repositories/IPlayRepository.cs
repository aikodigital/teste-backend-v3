using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Interfaces.Repositories
{
    public interface IPlayRepository : IBaseRepository<Play>
    {
        Task<Play> GetByNameAsync(string name);
    }
}
