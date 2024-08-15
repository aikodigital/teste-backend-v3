using System;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application
{
    public class ComedyCalculator : IPlayTypeCalculator
    {
        public bool CanHandle(string playType)
        {
            return playType == "comédia";
        }

        public decimal CalculateAmount(Play play, Performance performance)
        {
            try
            {
                var baseAmount = play.Price / 10;
                if (performance.Audience > 20)
                {
                    baseAmount += 100 + 5 * (performance.Audience - 20);
                }
                baseAmount += 3 * performance.Audience;

                return baseAmount;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao calcular o valor para comédia.", ex);
            }
        }

        public int CalculateCredits(Play play, Performance performance)
        {
            try
            {
                var credits = Math.Max(performance.Audience - 30, 0);
                credits += (int)Math.Floor((decimal)performance.Audience / 5);
                return credits;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao calcular os créditos para comédia.", ex);
            }
        }
    }
}