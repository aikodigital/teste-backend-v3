using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static TheatricalPlayersRefactoringKata.Base;

namespace TheatricalPlayersRefactoringKata;

public class History
{
    public int CalculateAmount(int lines, int audience)
    {
    var tragedy = new Tragedy();
    var tragedyAmount = tragedy.CalculateAmount(lines, audience);

    var comedy = new Comedy();
    var comedyAmount = comedy.CalculateAmount(lines, audience);

    var thisAmount = tragedyAmount + comedyAmount;

    return thisAmount;
    }
}