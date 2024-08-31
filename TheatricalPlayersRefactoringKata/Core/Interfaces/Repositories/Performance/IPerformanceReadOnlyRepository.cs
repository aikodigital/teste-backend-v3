using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Communication.Response;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces.Repositories.Performance;

public interface IPerformanceReadOnlyRepository
{
    Task<IEnumerable<PerformanceResponse>> GetPerformances();
    Task<Entities.Performance> GetPerformancesById(Guid performanceId);
}
