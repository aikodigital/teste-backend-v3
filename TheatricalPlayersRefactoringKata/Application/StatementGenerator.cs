using System;
using System.Collections.Generic;
using System.Linq;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application
{
    public class StatementGenerator : IStatementGenerator
    {
        private readonly IEnumerable<IPlayTypeCalculator> _calculators;

        public StatementGenerator(IEnumerable<IPlayTypeCalculator> calculators)
        {
            _calculators = calculators;
        }

        public string Generate(Invoice invoice, Dictionary<string, Play> plays)
        {
            try
            {
                var totalAmount = 0m;
                var volumeCredits = 0;
                var result = $"Extrato para {invoice.Customer}\n";

                foreach (var perf in invoice.Performances)
                {
                    var play = plays[perf.PlayId];
                    var calculator = _calculators.FirstOrDefault(c => c.CanHandle(play.Type));

                    if (calculator == null)
                    {
                        throw new ArgumentException($"Nenhum calculador disponível para o tipo de peça: {play.Type}");
                    }

                    var thisAmount = calculator.CalculateAmount(play, perf);
                    volumeCredits += calculator.CalculateCredits(play, perf);

                    result += $"  {play.Name}: {thisAmount:C} ({perf.Audience} espectadores)\n";
                    totalAmount += thisAmount;
                }

                result += $"Valor total devido é {totalAmount:C}\n";
                result += $"Você ganhou {volumeCredits} créditos\n";
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao gerar o extrato.", ex);
            }
        }
    }
}