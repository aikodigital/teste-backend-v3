using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Formatters
{
    public class TextStatementFormatter : IStatementFormatter
    {
        public string Format(Invoice invoice)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Statement for {invoice.Customer}");

            foreach (var performance in invoice.Performances)
            {
                sb.AppendLine($"  {performance.Play.Name}: {performance.Credits.ToString("C2", CultureInfo.InvariantCulture)} ({performance.Audience} seats)");
            }

            sb.AppendLine($"Amount owed is {invoice.TotalCosts.ToString("C2", CultureInfo.InvariantCulture)}");
            sb.AppendLine($"You earned {invoice.TotalCredits} credits");

            return sb.ToString();
        }
    }
}
