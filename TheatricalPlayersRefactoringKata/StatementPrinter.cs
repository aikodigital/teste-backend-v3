using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            decimal totalAmount = 0;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var lines = play.Lines;

                // Ajusta o número de linhas para estar no intervalo entre 1000 e 4000
                if (lines < 1000) lines = 1000;
                if (lines > 4000) lines = 4000;

                // Calcula o valor base
                decimal thisAmount = (decimal)lines / 10;

                // Ajusta o valor baseado no tipo de peça
                switch (play.Type)
                {
                    case "tragedy":
                        thisAmount = CalculateTragedyAmount(lines, perf.Audience);
                        break;

                    case "comedy":
                        thisAmount = CalculateComedyAmount(lines, perf.Audience);
                        break;

                    case "history":
                        // Para peças históricas, o valor é a soma dos valores de tragédia e comédia
                        var tragedyAmount = CalculateTragedyAmount(lines, perf.Audience);
                        var comedyAmount = CalculateComedyAmount(lines, perf.Audience);

                        // O valor histórico não deve ser duplicado, então aplicamos as regras individualmente
                        thisAmount = tragedyAmount + comedyAmount;
                        break;

                    default:
                        throw new Exception("unknown type: " + play.Type);
                }

                // Adiciona os créditos
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                if (play.Type == "comedy")
                {
                    volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
                }

                // Adiciona linha para esta performance
                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, perf.Audience);
                totalAmount += thisAmount; // Agora totalAmount mantém a precisão decimal
            }

            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }

        private decimal CalculateTragedyAmount(int lines, int audience)
        {
            decimal amount = (decimal)lines / 10;
            if (audience > 30)
            {
                amount += 10 * (audience - 30);
            }
            return amount;
        }

        private decimal CalculateComedyAmount(int lines, int audience)
        {
            decimal amount = (decimal)lines / 10 + 3 * audience;
            if (audience > 20)
            {
                amount += 100 + 5 * (audience - 20);
            }
            return amount;
        }

        public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
        {
            var xmlPrinter = new XmlStatementPrinter();
            return xmlPrinter.PrintXml(invoice, plays);
        }
    }
}