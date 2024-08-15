using System;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application
{
    public class TragedyCalculator : IPlayTypeCalculator
    {
        public bool CanHandle(string playType)
        {
            return playType == "tragédia";
        }

        public decimal CalculateAmount(Play play, Performance performance)
        {
            try
            {
                var baseAmount = play.Price / 10;
                if (performance.Audience > 30)
                {
                    baseAmount += 10 * (performance.Audience - 30);
                }
                return baseAmount;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao calcular o valor para tragédia.", ex);
            }
        }

        public int CalculateCredits(Play play, Performance performance)
        {
            try
            {
                return Math.Max(performance.Audience - 30, 0);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao calcular os créditos para tragédia.", ex);
            }
        }
    }
}