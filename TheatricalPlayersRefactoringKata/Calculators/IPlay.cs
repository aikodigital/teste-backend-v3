using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public interface IPlay
    {
        double CalculateAmount(Play play, Performance performance);
        int CalculateVolumeCredits(Performance performance);
    }
}
