using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersRefactoringKata;

public class Base
{
    public decimal CalculateBaseAmount(int lines)
    {
        if (lines < 1000) lines = 1000;
        if (lines > 4000) lines = 4000;

        decimal baseAmount = lines * 10;

        return baseAmount;
    }
}