using System.Threading.Tasks;
using System;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Gender;

public class TragedyCalculator : IPerformanceCalculator
{
    public Task<decimal> CalculateAmount(Performance performance, Play play)
    {
        // Calcula o valor base considerando as linhas da peça
        var lines = Math.Clamp(play.Lines, 1000, 4000);
        decimal amount = lines * 10M;  // Valor base

        // Adiciona um valor adicional se o número de espectadores for maior que 30
        if (performance.Audience > 30)
        {
            amount += 1000M * (performance.Audience - 30);  // Incremento para audiências acima de 30
        }

        // Retorna o valor calculado, ajustado para o formato correto
        return Task.FromResult(amount / 100);  // Dividido por 100 para converter centavos em reais/dólares
    }

    public Task<int> CalculateVolumeCredits(Performance performance, Play play)
    {
        // Calcula os créditos de volume considerando a audiência
        int credits = Math.Max(performance.Audience - 30, 0);
        return Task.FromResult(credits);
    }
}
