using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Interfaces;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class ComedyCalculator : IMainCalculator
    {
        public int CalculateAmount(Play play, Performance perf)
        {
            int lines = Math.Clamp(play.Lines, Constants.MIN_LINES, Constants.MAX_LINES);
            int amount = lines * Constants.BASE_COST_PER_LINE;

            if (perf.Audience > Constants.COMEDY_BONUS_THRESHOLD)
            {
                amount += Constants.COMEDY_BASE_COST + Constants.COMEDY_EXTRA_COST_PER_AUDIENCE * (perf.Audience - Constants.COMEDY_BONUS_THRESHOLD);
            }

            amount += Constants.COMEDY_ADDITIONAL_COST_PER_AUDIENCE * perf.Audience;

            return amount;
        }

        public int CalculateVolumeCredits(Play play, Performance perf)
        {
            int credits = Math.Max(perf.Audience - Constants.CREDIT_THRESHOLD, 0);
            credits += (int)Math.Floor((decimal)perf.Audience / Constants.COMEDY_BONUS_FACTOR);
            return credits;
        }
    }

}
