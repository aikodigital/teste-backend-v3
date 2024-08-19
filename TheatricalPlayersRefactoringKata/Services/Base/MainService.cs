using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services.Base
{
    public abstract class MainService
        {
        public int CalculateAmount(Play play, Performance perf)
            {
              var calculator = PlayCalculatorFactory.GetCalculator(play.Type);
              return calculator.CalculateAmount(play, perf);
            }

        public int CalculateVolumeCredits(Play play, Performance perf)
            {
              var calculator = PlayCalculatorFactory.GetCalculator(play.Type);
              return calculator.CalculateVolumeCredits(play, perf);
            }
        }

}
