using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.OutputStrategies
{
    public interface IStatementOutputStrategy
    {
        string Format(Invoice invoice, Dictionary<string, Play> plays, decimal totalAmount, int totalCredits);
    }
}
