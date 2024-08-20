using System.Globalization;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Formatters
{
    public class TextStatementFormatter : IStatementFormatter
    {
        public string Format(Invoice invoice)

        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            var result = string.Format("Statement for {0}\n", invoice.Customer);

            foreach (var perf in invoice.Performances)
            {
                var play = perf.Play;
                var baseAmount = play.Amount;
                result += FormatPerformanceLine(cultureInfo, perf, play, baseAmount);
            }

            result += FormatTotalAmount(invoice.TotalAmount, cultureInfo);
            result += FormatVolumeCredits(invoice.TotalCredits);
            return result;

        }

        private string FormatPerformanceLine(CultureInfo cultureInfo, Performance perf, Play play, decimal baseAmount)
        {
            return string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(baseAmount / 100), perf.Audience);
        }

        private static string FormatVolumeCredits(int volumeCredits)
        {
            return string.Format("You earned {0} credits\n", volumeCredits);
        }

        private static string FormatTotalAmount(decimal totalAmount, CultureInfo cultureInfo)
        {
            return string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        }
    }
}
