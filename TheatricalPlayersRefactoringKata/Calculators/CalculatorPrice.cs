using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    class CalculatorPrice
    {
        public decimal CalculateAmount(Play play, Performance perf)
        {
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var baseAmount = lines / 10.0m;
            var thisAmount = baseAmount;
            switch (play.Type)
            {
                case "tragedy":
                    if (perf.Audience > 30)
                    {
                        thisAmount += 10 * (perf.Audience - 30);
                    }
                    break;
                case "comedy":
                    thisAmount += 3 * perf.Audience;
                    if (perf.Audience > 20)
                    {
                        thisAmount += 100 + 5 * (perf.Audience - 20);
                    }
                    break;
                case "history":
                    var tragedyAmount = baseAmount;
                    if (perf.Audience > 30)
                    {
                        tragedyAmount += 10 * (perf.Audience - 30);
                    }

                    var comedyAmount = baseAmount + 3 * perf.Audience;
                    if (perf.Audience > 20)
                    {
                        comedyAmount += 100 + 5 * (perf.Audience - 20);
                    }

                    thisAmount = tragedyAmount + comedyAmount;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            return Math.Round(thisAmount, 2);
        }

        public int CalculateVolumeCredits(Play play, Performance perf)
        {
            var credits = Math.Max(perf.Audience - 30, 0);
            if (play.Type == "comedy")
            {
                credits += (int)Math.Floor((decimal)perf.Audience / 5);
            }
            return credits;
        }

        public decimal CalculateTotalAmount(Invoice invoice, Dictionary<string, Play> plays)
        {
            return Math.Round(invoice.Performances.Sum(perf => CalculateAmount(plays[perf.PlayId], perf)), 2);
        }

        public int CalculateTotalVolumeCredits(Invoice invoice, Dictionary<string, Play> plays)
        {
            return invoice.Performances.Sum(perf => CalculateVolumeCredits(plays[perf.PlayId], perf));
        }
    }
}
