using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services
{
    public class AmountCalculator
    {
        private PlayTypeCalculatorFactory _factory = new PlayTypeCalculatorFactory();

        public decimal CalculateAmount(Play play, Performance performance)
        {
            IPlayTypeCalculator calculator = _factory.GetCalculator(play.Type);
            return calculator.CalculateAmount(play, performance);
        }
    }
}
