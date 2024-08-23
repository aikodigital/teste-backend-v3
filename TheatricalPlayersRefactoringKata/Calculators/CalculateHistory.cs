using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Interfaces;
namespace TheatricalPlayersRefactoringKata.Calculators
{
  internal class CalculateHistory : ICalculateStrategy
    {
        private readonly CalculateComedy calculateComedy = new CalculateComedy();
        private readonly CalculateTragedy calculateTragedy = new CalculateTragedy();
        public decimal CalculateAmount(Play play, Performance performance)
        {
            decimal amount = 0.00m;
            amount += calculateComedy.CalculateAmount(play, performance);
            amount += calculateTragedy.CalculateAmount(play, performance); 
            return amount;
        }

        public int CalculateCredits(Play play, Performance performance)
        {
            var credits = Math.Max(performance.Audience - 30, 0);
            return credits;
        }
    }
}