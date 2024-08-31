using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces.Repositories.Performance;

public interface IPerformanceWriteOnlyRepository
{
    Task AddPerfomanceAsync(Entities.Performance performance);

}
