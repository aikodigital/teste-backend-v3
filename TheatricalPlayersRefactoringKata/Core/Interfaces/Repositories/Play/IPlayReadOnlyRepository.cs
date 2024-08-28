using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces.Repositories.Play;

public interface IPlayReadOnlyRepository
{
    Task<Entities.Play> GetPlayByIdAsync(string playId);
    Task<IDictionary<string, Entities.Play>> GetAllPlaysAsync();
}
