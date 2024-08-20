using System.Numerics;
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

    public string Print(Invoice invoice)
    {
        foreach(var perf in invoice.Performances) 
        {
            var play = perf.Play;
            int amount = 0;

            switch (play.Genre) 
            {
                case Genre.Tragedy:
                    amount = TragedyAmountCalculator.Calculate(perf, play);
                    break;
                case Genre.Comedy:
                    amount = ComedyAmountCalculator.Calculate(perf, play);
                    break;
                case Genre.History:
                    amount = HistoryAmountCalculator.Calculate(perf, play);
                    break;
                default:
                    throw new Exception("unknown type: " + play.Genre);
            }

            play.Amount = amount;
            invoice.TotalCredits += perf.CalculateVolumeCredits(play.Genre);
            invoice.TotalAmount += amount;
        }

        return _formatter.Format(invoice);

    }
}
