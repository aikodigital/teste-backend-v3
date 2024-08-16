using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    internal class AmountCalculator
    {
        private readonly PlayCalculatorFactory _factory = new PlayCalculatorFactory();

        public decimal CalculateAmount(Play play, Performance perf)
        {
            var calculator = _factory.GetCalculator(play.Type);
            return calculator.CalculateAmount(play, perf);
        }
    }
}
