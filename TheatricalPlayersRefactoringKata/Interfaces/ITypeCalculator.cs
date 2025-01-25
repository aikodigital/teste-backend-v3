using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces
{
    public interface ITypeCalculator
    {
        double Calculate(Performance performance);
        double CalculateCredits(Performance performance);
    }
}
