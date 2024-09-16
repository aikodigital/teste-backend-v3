using System;
using TheatricalPlayersRefactoringKata.Enum;

namespace TheatricalPlayersRefactoringKata.Entities;

public class PerformanceEntity
{
    public string PlayId { get; private set; }

    public int Audience { get; private set; }

    public PerformanceEntity(string playId, int audience)
    {
        PlayId = playId;
        Audience = audience;
    }

    /// <summary>
    /// Calculate amount.
    /// </summary>
    /// <param name="play">Play entity.</param>
    /// <returns>Performance amount.</returns>
    /// <exception cref="Exception">If an invalid <see cref="PlayTypeEnum">play type</see> is provided.</exception>
    public decimal GetAmount(PlayEntity play)
    {
        var lines = play.Lines switch
        {
            < 1000 => 1000,
            > 4000 => 4000,
            _ => play.Lines
        };

        var baselineAmountUnit = lines / 10m;
        var amount = baselineAmountUnit * 100;
        switch (play.Type)
        {
            case PlayTypeEnum.Tragedy:
                if (Audience > 30)
                {
                    amount += 1000 * (Audience - 30);
                }

                break;

            case PlayTypeEnum.Comedy:
                amount += 300 * Audience;

                if (Audience > 20)
                {
                    amount += 10000 + 500 * (Audience - 20);
                }

                break;

            case PlayTypeEnum.History:
                var tragedyAmount = amount;
                if (Audience > 30)
                {
                    tragedyAmount += 1000 * (Audience - 30);
                }

                var comedyAmount = amount + 300 * Audience;
                if (Audience > 20)
                {
                    comedyAmount += 10000 + 500 * (Audience - 20);
                }

                amount = tragedyAmount + comedyAmount;

                break;

            default:
                throw new Exception("unknown type: " + play.Type);
        }

        return amount;
    }

    /// <summary>
    /// Calculate volume credits.
    /// </summary>
    /// <param name="play">Play entity.</param>
    /// <returns>Volume credits.</returns>
    public int GetVolumeCredits(PlayEntity play)
    {
        var volumeCredits = Math.Max(Audience - 30, 0);

        if (play.Type == PlayTypeEnum.Comedy)
        {
            volumeCredits += (int)Math.Floor((decimal)Audience / 5);
        }

        return volumeCredits;
    }
}