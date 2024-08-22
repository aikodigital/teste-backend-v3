using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public interface IStatementCalculator
    {
        decimal CalculateTotalAmount(Invoice invoice, Dictionary<string, Play> plays);
        int CalculateTotalCredits(Invoice invoice);
        IPlayCategory GetPlayCategory(string category);
    }
}
