using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static TheatricalPlayersRefactoringKata.Base;

namespace TheatricalPlayersRefactoringKata;

public class History
{
    public decimal CalculateAmount(int lines, int audience)
    {
        var tragedy = new Tragedy();
        decimal tragedyAmount = tragedy.CalculateAmount(lines, audience);

        var comedy = new Comedy();
        decimal comedyAmount = comedy.CalculateAmount(lines, audience);

        decimal thisAmount = tragedyAmount + comedyAmount;

        return thisAmount;
    }
}