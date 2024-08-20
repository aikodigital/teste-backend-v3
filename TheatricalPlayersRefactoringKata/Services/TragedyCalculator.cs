using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Interfaces;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class TragedyCalculator : IMainCalculator
    {
        public int CalculateAmount(Play play, Performance perf)
        {
            int lines = Math.Clamp(play.Lines, Constants.MIN_LINES, Constants.MAX_LINES);
            int amount = lines * Constants.BASE_COST_PER_LINE;

            if (perf.Audience > Constants.CREDIT_THRESHOLD)
            {
                amount += Constants.TRAGEDY_EXTRA_COST_PER_AUDIENCE * (perf.Audience - Constants.CREDIT_THRESHOLD);
            }

            return amount;
        }

        public int CalculateVolumeCredits(Play play, Performance perf)
        {
            return Math.Max(perf.Audience - Constants.CREDIT_THRESHOLD, 0);
        }
    }

}
