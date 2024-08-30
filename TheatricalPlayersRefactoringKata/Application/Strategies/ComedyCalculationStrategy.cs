using System;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Application.Strategies;

public class ComedyCalculationStrategy : ICalculationStrategy
{
    public decimal CalculateAmount(Performance perf, Play play)
    {
        // TODO: Achar alguma forma de isolar essa logica
        var lines = play.Lines;
        lines = Math.Max(1000, Math.Min(lines, 4000));
        var thisAmount = lines * 10;
        // TODO: FIM

        if (perf.Audience > 20)
        {
            thisAmount += 10000 + 500 * (perf.Audience - 20);
        }
        thisAmount += 300 * perf.Audience;
        return thisAmount;
    }

    public decimal CalculateCredits(Performance perf, Play play) => 
        Math.Max(perf.Audience - 30, 0) + (int)Math.Floor((decimal)perf.Audience / 5);
    
}

