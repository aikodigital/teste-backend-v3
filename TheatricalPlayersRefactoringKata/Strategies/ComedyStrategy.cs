using System;
using TheatricalPlayersRefactoringKata.Interfaces;
using TheatricalPlayersRefactoringKata;

public class ComedyStrategy : IStatementStrategy
{
    public decimal CalculatePrice(Play play, Performance perf)
    {
        if (perf.Audience <= 0)
            return 0;

        var lines = play.Lines;

        if (lines < 1000) lines = 1000;
        if (lines > 4000) lines = 4000;

        var thisAmount = lines * 10;

        if (perf.Audience > 20)
        {
            thisAmount += 10000 + 500 * (perf.Audience - 20);
        }

        thisAmount += 300 * perf.Audience;

        return thisAmount;
    }

    public int CalculateCredits(Play play, Performance perf)
    {
        var credits = Math.Max(perf.Audience - 30, 0);

        if (perf.Audience > 20)
        {
            credits += (int)Math.Floor((decimal)perf.Audience / 5);
        }

        return credits;
    }
}
