using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services
{
    public class HistoryCalculator : IPlayTypeCalculator
    {
        private TragedyCalculator _tragedyCalculator = new TragedyCalculator();
        private ComedyCalculator _comedyCalculator = new ComedyCalculator();

        public decimal CalculateAmount(Play play, Performance performance)
        {
            decimal tragedyAmount = _tragedyCalculator.CalculateAmount(play, performance);
            decimal comedyAmount = _comedyCalculator.CalculateAmount(play, performance);

            return tragedyAmount + comedyAmount;
        }

        public int CalculateCredits(Performance performance)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}
