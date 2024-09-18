using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Calculators.Interface;
using TheatricalPlayersRefactoringKata.Factory;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators;

public class HistoryPlayCalculator : ICalculator
{
    public decimal CalculateAmount(Performance performance, Play play)
    {
        ICalculator comedyCalculator = PlayCalculatorFactory.GetCalculator("comedy");
        ICalculator tragedyCalculator = PlayCalculatorFactory.GetCalculator("tragedy");

        decimal comedyAmount = comedyCalculator.CalculateAmount(performance, play);
        decimal tragedyAmount = tragedyCalculator.CalculateAmount(performance, play);

        return comedyAmount + tragedyAmount;
    }

    public int CalculateCredits(Performance performance)
    {
        var volumeCredits = 0;
        return volumeCredits += Math.Max(performance.Audience - 30, 0);
    }
}

