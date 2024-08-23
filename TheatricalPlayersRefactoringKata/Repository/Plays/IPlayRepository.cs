using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Repository.Plays
{
    public interface IPlayRepository
    {
        Task<Play> GetByName(string playName);
        Task<IEnumerable<Play>> GetAll();
        Task<Play> Create(Play play);
        Task<Play> Update(Play playEdited);
        void DeleteByName(string playName);
    }
}
