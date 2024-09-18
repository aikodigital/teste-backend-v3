using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata
{
    public class PlayCalculatorFactory
    {
        public static IPlayCalculator createCalculator(string type)
        {
            return type switch
            {
                "tragedy" => new TragedyCalculator(),
                "comedy" => new ComedyCalculator(),
                "history" => new HistoricalCalculator(),
                _ => throw new Exception("unknown type: " + type)
            };
        }
    }
}
