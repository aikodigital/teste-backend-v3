using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TheatricalPlayersRefactoringKata
{
    public class XmlStatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            decimal totalAmount = 0m;
            decimal volumeCredits = 0;
            StringBuilder result = new StringBuilder();
            CultureInfo cultureInfo = new CultureInfo("en-US");

            // Início do XML<?xml version="1.0" encoding="utf-8"?>
            result.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            result.AppendLine("<Statement xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
            result.AppendLine($"  <Customer>{invoice.Customer}</Customer>");
            result.AppendLine("  <Items>");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];

                decimal lines = play.Lines;
                lines = Math.Max(1000, Math.Min(lines, 4000));

                decimal thisAmount = lines * 10;

                switch (play.Type)
                {
                    case "tragedy":
                        if (perf.Audience > 30)
                        {
                            thisAmount += 1000m * (perf.Audience - 30);
                        }
                        break;
                    case "comedy":
                        if (perf.Audience > 20)
                        {
                            thisAmount += 10000m + 500m * (perf.Audience - 20);
                        }
                        thisAmount += 300m * perf.Audience;
                        break;
                    case "history":
                        decimal tragedyAmount = thisAmount;
                        decimal comedyAmount = thisAmount + 300m * perf.Audience;

                        if (perf.Audience > 30)
                        {
                            tragedyAmount += 1000m * (perf.Audience - 30);
                        }
                        if (perf.Audience > 20)
                        {
                            comedyAmount += 10000m + 500m * (perf.Audience - 20);
                        }

                        thisAmount = tragedyAmount + comedyAmount;
                        break;
                    default:
                        throw new Exception("unknown type: " + play.Type);
                }

                // Adicionar volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);

                // Adicionar extra credit para cada dez espectadores de comédia
                if ("comedy" == play.Type)
                    volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

                // Adicionar item ao XML
                result.AppendLine("    <Item>");
                result.AppendLine($"      <AmountOwed>{FormatAmount(thisAmount / 100)}</AmountOwed>");
                result.AppendLine($"      <EarnedCredits>{(play.Type == "comedy" ? (int)Math.Floor((decimal)perf.Audience / 5) + Math.Max(perf.Audience - 30, 0) : Math.Max(perf.Audience - 30, 0))}</EarnedCredits>");
                result.AppendLine($"      <Seats>{perf.Audience}</Seats>");
                result.AppendLine("    </Item>");

                totalAmount += thisAmount;
            }

            result.AppendLine("  </Items>");
            result.AppendLine($"  <AmountOwed>{FormatAmount(totalAmount / 100)}</AmountOwed>");
            result.AppendLine($"  <EarnedCredits>{volumeCredits}</EarnedCredits>");
            result.AppendLine("</Statement>");

            return result.ToString();
        }

        private string FormatAmount(decimal amount)
        {
            // Verifica se o valor é inteiro
            return amount % 1 == 0 ? amount.ToString("0", CultureInfo.InvariantCulture) : amount.ToString("0.0", CultureInfo.InvariantCulture);
        }
    }
}
