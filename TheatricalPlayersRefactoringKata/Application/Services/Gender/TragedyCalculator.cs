using System;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Gender;

public class TragedyCalculator : IPerformanceCalculator
{
    public Task<int> CalculateAmount(Performance performance, Play play)
    {
        var lines = Math.Clamp(play.Lines, 1000, 4000);
        int amount = lines * 10;
        if (performance.Audience > 30)
        {
            amount += 1000 * (performance.Audience - 30);
        }
        return Task.FromResult(amount);
    }

    public Task<int> CalculateVolumeCredits(Performance performance, Play play)
    {
        int credits = Math.Max(performance.Audience - 30, 0);
        return Task.FromResult(credits);
    }
}

