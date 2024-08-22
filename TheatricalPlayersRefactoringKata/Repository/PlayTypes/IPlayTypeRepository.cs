using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Repository
{
    public interface IPlayTypeRepository
    {
        Task<PlayType> GetByName(string typeName);
        Task<IEnumerable<PlayType>> GetAll();
        Task<PlayType> Create(PlayType playType);
        Task<PlayType> Update(PlayType playType);
        void DeleteByName(string typeName);
    }
}
