using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Constants;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class CalculateAdditionalValuePerPlayType : ICalculateAdditionalValuePerPlayType
    {
        public decimal CalculateAdditionalValue(int audience, int audienceMinimum, decimal bonus, decimal per_audience_additional, decimal per_audience)
        {
            var thisAmount = 0m;

            if (audience > audienceMinimum)
            {
                thisAmount += bonus
                              + per_audience_additional
                              * (audience - audienceMinimum);
            }

            if (per_audience > 0) thisAmount += per_audience * audience;

            return thisAmount;
        }
    }
}
