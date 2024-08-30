using System;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Gender
{
    public class ComedyCalculator : IPerformanceCalculator
    {
        public Task<decimal> CalculateAmount(Performance performance, Play play)
        {
            // Calcula o valor base considerando as linhas da peça
            var lines = Math.Clamp(play.Lines, 1000, 4000);
            decimal baseAmount = lines * 10M;  // Valor base

            // Adiciona $3.00 por espectador
            decimal amount = baseAmount + 300M * performance.Audience;

            // Adiciona valores adicionais para audiências acima de 20
            if (performance.Audience > 20)
            {
                amount += 10000M + 500M * (performance.Audience - 20);  // $100.00 + $5.00 por espectador acima de 20
            }

            // Retorna o valor calculado, ajustado para o formato correto
            return Task.FromResult(amount / 100);  // Dividido por 100 para converter centavos em reais/dólares
        }

        public Task<int> CalculateVolumeCredits(Performance performance, Play play)
        {
            // Calcula os créditos de volume considerando a audiência
            int credits = Math.Max(performance.Audience - 30, 0);
            credits += (int)Math.Floor((decimal)performance.Audience / 5);  // Bônus de créditos para comédia
            return Task.FromResult(credits);
        }
    }
}
