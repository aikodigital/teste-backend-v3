using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Formatters
{
    public class TextStatementFormatter : IStatementFormatter
    {
        public TextStatementFormatter() { }

        public string Format(Invoice invoice, Dictionary<string, Play> plays, Dictionary<string, IStatementStrategy> strategies)
        {
            var totalAmount = 0m; // Usa decimal para totalAmount para manter precisão
            var volumeCredits = 0;
            var result = $"Statement for {invoice.Customer}\n";

            // Define uma cultura personalizada com o símbolo do dólar e ponto como separador decimal
            var cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.CurrencySymbol = "$"; // Usa o símbolo do dólar para a moeda
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = "."; // Usa um ponto como separador decimal
            cultureInfo.NumberFormat.CurrencyGroupSeparator = ","; // Usa uma vírgula como separador de milhar
            cultureInfo.NumberFormat.CurrencyDecimalDigits = 2; // Duas casas decimais

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];

                if (!strategies.TryGetValue(play.Type, out var strategy))
                {
                    throw new Exception("Unknown play type: " + play.Type); // Lança exceção se o tipo de peça não for conhecido
                }

                var thisAmount = strategy.CalculatePrice(play, perf); // thisAmount é decimal
                var credits = strategy.CalculateCredits(play, perf);
                volumeCredits += credits;

                totalAmount += thisAmount; // Acumula o valor total
                var correctedAmount = thisAmount / 100;
                // Imprime a linha para esta ordem
                result += $"  {play.Name}: {correctedAmount.ToString("C", cultureInfo)} ({perf.Audience} seats)\n"; // Usa a cultura personalizada
            }
            var totalAmountCorrected = totalAmount / 100;
            // Formata o valor total e os créditos de volume
            result += $"Amount owed is {totalAmountCorrected.ToString("C", cultureInfo)}\n";
            result += $"You earned {volumeCredits} credits\n";

            return result;
        }
    }
}
