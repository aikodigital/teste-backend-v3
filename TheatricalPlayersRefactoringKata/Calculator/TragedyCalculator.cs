using System;
using TheatricalPlayersRefactoringKata.Helper;
using TheatricalPlayersRefactoringKata.Interface;
using TheatricalPlayersRefactoringKata.Model;

namespace TheatricalPlayersRefactoringKata.Calculator;
public class TragedyCalculator : IPlayCalculator
{
    public decimal CalculateAmount(Performance performance, Play play)
    {
        int lines = AjustaLinhasHelper.AjustaLimiteMinMaxDeLinhas(play);

        decimal amount = lines * 10;
        if (performance.Audience > 30)
        {
            amount += 1000 * (performance.Audience - 30);
        }
        return amount;
    }


    public int CalculateVolumeCredits(Performance performance) => Math.Max(performance.Audience - 30, 0);
}

