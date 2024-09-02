using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Infrastructure.Services;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class PlayTypeCalculatorFactory
    {
        public IPlayTypeCalculator GetCalculator(string playType)
        {
            switch (playType)
            {
                case "tragedy":
                    return new TragedyCalculator();
                case "comedy":
                    return new ComedyCalculator();
                case "history":
                    return new HistoryCalculator();
                default:
                    throw new ArgumentException($"Invalid play type: {playType}", nameof(playType));
            }
        }
    }
}
