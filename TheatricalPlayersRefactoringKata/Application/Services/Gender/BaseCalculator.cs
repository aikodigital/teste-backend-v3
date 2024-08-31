using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Gender;

public abstract class BaseCalculator
{
    public abstract int CalculateAmount(Performance perf, Play play);

    protected int DefaultAmount(int lines)
    {
        return lines switch
        {
            < 1000 => 1000 * 10,
            > 4000 => 4000 * 10,
            _ => lines * 10
        };
    }
}