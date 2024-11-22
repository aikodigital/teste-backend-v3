using CrossCutting;
using System;

namespace Aplication.Services.Calculators
{
    public static class PriceCalculatorFactory
    {
        public static PriceCalculator GetCalculator(PlayType playType)
        {
            return playType switch
            {
                PlayType.tragedy => new TragedyCalculator(),
                PlayType.comedy => new ComedyCalculator(),
                PlayType.history => new HistoryCalculator(),
                _ => throw new ArgumentException("Invalid PlayType")
            };
        }
    }
}
