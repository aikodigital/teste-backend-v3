using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersRefactoringKata;

public class Comedy
{
    public int CalculateAmount(int lines, int audience)
    {
        var baseCalculator = new Base();
        var baseAmount = baseCalculator.CalculateBaseAmount(lines);
        if (audience > 20) {
            baseAmount += 10000 + 500 * (audience - 20);
        }
        baseAmount += 300 * audience;

        return baseAmount;
    }
    public int CalculateExtraCredits(int audience)
    {
        var credits = (int)Math.Floor((decimal)audience / 5);

        return credits;
    }
}