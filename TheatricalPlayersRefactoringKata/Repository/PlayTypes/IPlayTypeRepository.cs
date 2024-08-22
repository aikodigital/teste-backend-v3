using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Repository
{
    public interface IPlayTypeRepository
    {
        Task<PlayTypes> GetByName(string typeName);
        Task<IEnumerable<PlayTypes>> GetAll();
        Task<PlayTypes> Create(PlayTypes playType);
        Task<PlayTypes> Update(PlayTypes playType);
        void DeleteByName(string typeName);
    }
}
