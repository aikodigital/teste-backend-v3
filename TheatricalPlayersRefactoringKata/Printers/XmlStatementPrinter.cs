using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Core;

namespace TheatricalPlayersRefactoringKata
{
    public class XmlStatementPrinter
    {
        private readonly Dictionary<string, IPlayTypeCalculator> _calculators;

        public XmlStatementPrinter()
        {
            _calculators = new Dictionary<string, IPlayTypeCalculator>
            {
                { "comedy", new ComedyCalculator() },
                { "tragedy", new TragedyCalculator() },
                { "history", new HistoryCalculator() }
            };
        }
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            decimal totalAmount = 0m;
            decimal volumeCredits = 0;
            StringBuilder result = new StringBuilder();
            CultureInfo cultureInfo = new CultureInfo("en-US");

            result.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            result.AppendLine("<Statement xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
            result.AppendLine($"  <Customer>{invoice.Customer}</Customer>");
            result.AppendLine("  <Items>");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var calculator = _calculators[play.Type];

                decimal thisAmount = calculator.CalculateAmount(perf, play);
                volumeCredits += calculator.CalculateVolumeCredits(perf);

                result.AppendLine("    <Item>");
                result.AppendLine($"      <AmountOwed>{FormatAmount(thisAmount / 100)}</AmountOwed>");
                result.AppendLine($"      <EarnedCredits>{calculator.CalculateVolumeCredits(perf)}</EarnedCredits>");
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
            return amount % 1 == 0 ? amount.ToString("0", CultureInfo.InvariantCulture) : amount.ToString("0.0", CultureInfo.InvariantCulture);
        }
    }
}
