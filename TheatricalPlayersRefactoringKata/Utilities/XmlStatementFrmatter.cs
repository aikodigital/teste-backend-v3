using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Utilities;
public class XmlStatementFormatter : IStatementFormatter
{
    public string FormatStatement(Invoice invoice, Dictionary<string, Play> plays)
    {
        var sb = new StringBuilder();

        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        sb.AppendLine("<Statement xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
        sb.AppendLine($"  <Customer>{invoice.Customer}</Customer>");
        sb.AppendLine("  <Items>");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            double thisAmount = play.Calculator.CalculateAmount(perf, play);
            int volumeCredits = play.Calculator.CalculateCredits(perf);

            sb.AppendLine("    <Item>");
            sb.AppendLine($"      <AmountOwed>{FormatAmount(thisAmount / 100)}</AmountOwed>");
            sb.AppendLine($"      <EarnedCredits>{volumeCredits}</EarnedCredits>");
            sb.AppendLine($"      <Seats>{perf.Audience}</Seats>");
            sb.AppendLine("    </Item>");
        }

        sb.AppendLine("  </Items>");

        double totalAmount = 0;
        int totalVolumeCredits = 0;

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            totalAmount += play.Calculator.CalculateAmount(perf, play);
            totalVolumeCredits += play.Calculator.CalculateCredits(perf);
        }

        sb.AppendLine($"  <AmountOwed>{FormatAmount(totalAmount / 100)}</AmountOwed>");
        sb.AppendLine($"  <EarnedCredits>{totalVolumeCredits}</EarnedCredits>");

        sb.AppendLine("</Statement>");

        return sb.ToString();
    }

    private string FormatAmount(double amount)
    {
        // Formatar o valor de acordo com se é um número redondo ou não
        if (amount % 1 == 0)
        {
            // Sem casas decimais para números inteiros
            return amount.ToString("0", CultureInfo.InvariantCulture);
        }
        else
        {
            // Com uma casa decimal para números com decimais
            return amount.ToString("0.0", CultureInfo.InvariantCulture);
        }
    }

}