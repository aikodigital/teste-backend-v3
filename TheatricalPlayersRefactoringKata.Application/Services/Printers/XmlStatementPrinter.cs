using System.Text;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Services.Factories;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Printers
{
    public class XmlStatementPrinter : IStatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0m;
            var totalVolumeCredits = 0;
            var xml = new StringBuilder();

            xml.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xml.AppendLine("<Statement xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
            xml.AppendLine($"  <Customer>{invoice.Customer}</Customer>");
            xml.AppendLine("  <Items>");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var calculator = GenreCalculatorFactory.GetCalculator(play.Type);

                // Calcula o valor devido e os créditos ganhados para cada performance
                var thisAmount = calculator.CalculateAmount(perf, play);
                var volumeCredits = calculator.CalculateVolumeCredits(perf);

                totalAmount += thisAmount;
                totalVolumeCredits += volumeCredits;

                // Gera o XML para cada item
                xml.AppendLine("    <Item>");
                xml.AppendLine($"      <AmountOwed>{thisAmount / 100}</AmountOwed>");
                xml.AppendLine($"      <EarnedCredits>{volumeCredits}</EarnedCredits>");
                xml.AppendLine($"      <Seats>{perf.Audience}</Seats>");
                xml.AppendLine("    </Item>");
            }

            xml.AppendLine("  </Items>");
            xml.AppendLine($"  <AmountOwed>{totalAmount / 100}</AmountOwed>");
            xml.AppendLine($"  <EarnedCredits>{totalVolumeCredits}</EarnedCredits>");
            xml.Append("</Statement>");

            return xml.ToString();
        }
    }
}
