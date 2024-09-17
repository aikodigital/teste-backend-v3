using System;
using TheatricalPlayersRefactoringKata.Helper;
using TheatricalPlayersRefactoringKata.Interface;
using TheatricalPlayersRefactoringKata.Model;

namespace TheatricalPlayersRefactoringKata.Calculator;
public class ComedyCalculator : IPlayCalculator
{
    public decimal CalculateAmount(Performance performance, Play play)
    {
        int lines = AjustaLinhasHelper.AjustaLimiteMinMaxDeLinhas(play);

        decimal amount = lines * 10;
        amount += 300 * performance.Audience;
        if (performance.Audience > 20)
        {
            amount += 10000 + 500 * (performance.Audience - 20);
        }
        return amount;
    }

    public int CalculateVolumeCredits(Performance performance)
    {
        int credits = Math.Max(performance.Audience - 30, 0);
        credits += (int)Math.Floor((decimal)performance.Audience / 5);
        return credits;
    }
}

