using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Constants;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class CalculateCreditAudience : ICalculateCreditAudience
    {
        public decimal CalculateCredit(int audience, string gender)
        {
            // add volume credits
            var volumeCredits = Math.Max(audience - StatementPrinterConstants.CREDIT_MINIMUM_AUDIENCE, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == gender) volumeCredits += (int)Math.Floor(audience / StatementPrinterConstants.COMEDY_BONUS_CREDIT_PER_ATTENDEES);

            return volumeCredits;
        }
    }
}
