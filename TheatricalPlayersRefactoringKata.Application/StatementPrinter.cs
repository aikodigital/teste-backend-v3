using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Services.Calculators;

namespace TheatricalPlayersRefactoringKata.Application;

public class StatementPrinter
{
    private readonly IStatementFormatter _formatter;

    public StatementPrinter(IStatementFormatter formatter)
    {
        _formatter = formatter;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var performanceAmounts = new Dictionary<Performance, int>();
        var totalAmount = 0;
        var volumeCredits = 0;

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var baseAmount = play.CalculateBaseAmount();

            switch (play.Genre) 
            {
                case Genre.Tragedy:
                    baseAmount = TragedyAmountCalculator.Calculate(perf, play, baseAmount);
                    break;
                case Genre.Comedy:
                    baseAmount = ComedyAmountCalculator.Calculate(perf, play, baseAmount);
                    break;
                default:
                    throw new Exception("unknown type: " + play.Genre);
            }

            performanceAmounts[perf] = baseAmount;
            volumeCredits += perf.CalculateVolumeCredits(play.Genre);
            totalAmount += baseAmount;
        }

        return _formatter.Format(invoice, plays, performanceAmounts, volumeCredits, totalAmount);
    }
}
