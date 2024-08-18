using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.API.Repositories.Interfaces
{
    public interface IPlayRepository : IGenericRepository<Play>
    {
        Task<Play> GetByNameAsync(string name);
    }
}
