using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IPerformanceService
    {
        Task<Domain.Entities.Performance> CreatePerformanceAsync(Domain.Entities.Performance statement);
    }
}
