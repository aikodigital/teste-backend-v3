using System.Collections.Generic;
using System.Text;
using TheatricalPlayersRefactoringKata.Core;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata
{
    public class XmlStatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var xmlBuilder = new StringBuilder();

            xmlBuilder.AppendLine($"<Statement>");
            xmlBuilder.AppendLine($"  <Customer>{invoice.Customer}</Customer>");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var calculator = CalculatorFactory.CreateCalculator(play);
                var thisAmount = calculator.CalculateAmount(perf, play);

                // Adicionando créditos
                volumeCredits += calculator.CalculateCredits(perf);

                // Adicionando performance ao XML
                xmlBuilder.AppendLine($"  <Performance>");
                xmlBuilder.AppendLine($"    <PlayName>{play.Name}</PlayName>");
                xmlBuilder.AppendLine($"    <Amount>{thisAmount / 100.0:C}</Amount>");
                xmlBuilder.AppendLine($"    <Seats>{perf.Audience}</Seats>");
                xmlBuilder.AppendLine($"  </Performance>");

                totalAmount += thisAmount;
            }

            xmlBuilder.AppendLine($"  <TotalAmount>{totalAmount / 100.0:C}</TotalAmount>");
            xmlBuilder.AppendLine($"  <VolumeCredits>{volumeCredits}</VolumeCredits>");
            xmlBuilder.AppendLine($"</Statement>");

            return xmlBuilder.ToString();
        }
    }
}
