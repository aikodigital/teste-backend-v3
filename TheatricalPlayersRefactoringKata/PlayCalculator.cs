using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata
{
    public class ComedyCalculator : IPlayCalculator
    {
        public decimal calculateAmount(Performance perf, decimal currentAmount)
        {
            if (perf.Audience > 20)
            {
                currentAmount += 100 + 5 * (perf.Audience - 20);
            }
            currentAmount += 3 * perf.Audience;

            return currentAmount;
        }
    }

    public class TragedyCalculator : IPlayCalculator
    {
        public decimal calculateAmount(Performance perf, decimal currentAmount)
        {
            if (perf.Audience > 30)
            {
                currentAmount += 10 * (perf.Audience - 30);
            }
            
            return currentAmount;
        }
    }

    public class HistoricalCalculator : IPlayCalculator
    {
        public decimal calculateAmount(Performance perf, decimal currentAmount)
        {
            IPlayCalculator tragedyCalc = new TragedyCalculator();
            IPlayCalculator comedyCalc = new ComedyCalculator();

            return tragedyCalc.calculateAmount(perf, currentAmount) + comedyCalc.calculateAmount(perf, currentAmount);
        }
    }
}
