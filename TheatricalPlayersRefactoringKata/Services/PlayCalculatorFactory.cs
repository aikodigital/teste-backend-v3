using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Services
{
    public static class PlayCalculatorFactory
    {
        public static IMainCalculator GetCalculator(string playType)
        {
            return playType switch
            {
                "tragedy" => new TragedyCalculator(),
                "comedy" => new ComedyCalculator(),
                "history" => new HistoryCalculator(),
                _ => throw new Exception($"Unknown play type: {playType}")
            };
        }
    }

}
