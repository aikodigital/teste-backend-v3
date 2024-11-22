using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces
{
    public interface IPerformanceRepository
    {
        Task<Entities.Performance> AddAsync(Entities.Performance performance);
    }
}
