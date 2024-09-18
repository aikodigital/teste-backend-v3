using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Calculators.Interface;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Factory;
public class PlayCalculatorFactory
{
    public static ICalculator GetCalculator(Play play)
    {
        return play.Type switch
        {
            "tragedy" => new TragedyPlayCalculator(),
            "comedy" => new ComedyPlayCalculator(),
            "history" => new HistoryPlayCalculator(),
            _ => throw new ArgumentException("Tipo de peça desconhecido"),
        };
    }

    public static ICalculator GetCalculator(string generoPeca)
    {
        return generoPeca switch
        {
            "tragedy" => new TragedyPlayCalculator(),
            "comedy" => new ComedyPlayCalculator(),
            "history" => new HistoryPlayCalculator(),
            _ => throw new ArgumentException("Tipo de peça desconhecido"),
        };
    }
}
