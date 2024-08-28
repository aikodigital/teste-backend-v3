using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public interface IPerformanceCalculator
    {
        int CalculateAmount(Performance performance, Play play);
        int CalculateVolumeCredits(Performance performance, Play play);
    }
}
