using System;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Core
{
    public interface ICalculator
    {
        int CalculateAmount(Performance performance, Play play);
        int CalculateCredits(Performance performance);
    }
}
