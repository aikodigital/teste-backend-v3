using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators.Interfaces
{
    public interface ITypeCalculator
    {
        double Calculate(Performance performance);
        double CalculateCredits(Performance performance);
    }
}
