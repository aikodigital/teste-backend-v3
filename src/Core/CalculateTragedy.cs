using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersRefactoringKata;

public class Tragedy
{
    public decimal CalculateAmount(int lines, int audience)
    {
        var baseCalculator = new Base();
        decimal baseAmount = baseCalculator.CalculateBaseAmount(lines);
        if (audience > 30) {
            baseAmount += 1000 * (audience - 30);
        }

        return baseAmount;
    }
}