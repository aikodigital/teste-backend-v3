using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators.Interfaces
{
    public interface IPlayCalculator
    {
        decimal CalculateAmount(Play play, int audience);
        int CalculateCredits(Play play, int audience);
    }
}
