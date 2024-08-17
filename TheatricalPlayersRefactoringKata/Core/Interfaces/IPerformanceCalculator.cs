using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces
{
    public interface IPerformanceCalculator
    {
        decimal CalculatePrice(Performance performance);
        int CalculateCredits(Performance performance);
    }
}
