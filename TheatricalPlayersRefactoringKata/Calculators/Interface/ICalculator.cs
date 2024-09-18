using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators.Interface;

public interface ICalculator
{
    decimal CalculateAmount(Performance performance, Play play);
    int CalculateCredits(Performance performance);
}

