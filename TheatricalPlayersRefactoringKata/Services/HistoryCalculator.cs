using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Interfaces;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class HistoryCalculator : IMainCalculator
    {
        public int CalculateAmount(Play play, Performance perf)
        {
            var tragedyCalculator = new TragedyCalculator();
            var comedyCalculator = new ComedyCalculator();

            int tragedyAmount = tragedyCalculator.CalculateAmount(play, perf);
            int comedyAmount = comedyCalculator.CalculateAmount(play, perf);

            return tragedyAmount + comedyAmount;
        }

        public int CalculateVolumeCredits(Play play, Performance perf)
        {
            return Math.Max(perf.Audience - Constants.CREDIT_THRESHOLD, 0);
        }
    }
}
