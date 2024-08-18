using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata
{
    public interface IChargeStrategy
    {
        int CalculateBilling(Performance performance, Play play);
        int CalculateCredits(Performance performance);
    }
}
