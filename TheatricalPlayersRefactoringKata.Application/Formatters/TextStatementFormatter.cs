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

            var culture = new CultureInfo("en-US");

            sb.AppendLine($"Statement for {invoice.Customer}");

            foreach (var performance in invoice.Performances)
            {
                sb.AppendLine($"  {performance.Play.Name}: {performance.Cost.ToString("C2", culture)} ({performance.Audience} seats)");
            }

            sb.AppendLine($"Amount owed is {invoice.TotalCosts.ToString("C2", culture)}");
            sb.AppendLine($"You earned {invoice.TotalCredits} credits");

            return sb.ToString();
        }
    }
}
