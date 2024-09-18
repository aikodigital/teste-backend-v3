using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata
{
    public class Calculates
    {
        public static decimal CalculatePlayAmount(Performance perf, Play play)
        {
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            decimal thisAmount = lines* 10;
            switch (play.Type)
            {
                case "tragedy":
                    if (perf.Audience > 30)
                    {
                        thisAmount += 1000 * (perf.Audience - 30);
                    }
                    break;
                case "comedy":
                    if (perf.Audience > 20)
                    {
                        thisAmount += 10000 + 500 * (perf.Audience - 20);
                    }
                    thisAmount += 300 * perf.Audience;
                    break;
                case "history":
                    var tragedyAmount = thisAmount;
                    if (perf.Audience > 30)
                    {
                        tragedyAmount += 1000 * (perf.Audience - 30);
                    }

                    var comedyAmount = thisAmount + 300 * perf.Audience;
                    if (perf.Audience > 20)
                    {
                        comedyAmount += 10000 + 500 * (perf.Audience - 20);
                    }

                    thisAmount = tragedyAmount + comedyAmount;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            return thisAmount;
        }
        public static int CalculateVolumeCredits(Performance perf, Play play)
        {
            int volumeCredits = Math.Max(perf.Audience - 30, 0);
            if ("comedy" == play.Type)
            {
                volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
            }
            return volumeCredits;
        }
    }
}
