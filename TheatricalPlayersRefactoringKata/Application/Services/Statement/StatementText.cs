using System.Text;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Strategy;

namespace TheatricalPlayersRefactoringKata.Application.Services.Statement
{
    public class StatementText : IStatement
    {
        public string Print(Invoice invoice)
        {
            StringBuilder sb = new();

            var customerName = invoice.Customer.Name;
            var perfomances = invoice.Performances.Select(inv => inv.Value);

            sb.Append(InvoiceHeaderDefault(customerName));
            sb.Append(InvoiceBodyDefault(perfomances));
            sb.Append(InvoiceFooterDefault(perfomances));

            return sb.ToString();
        }

        private static string InvoiceHeaderDefault(string customerName)
        {
            return string.Format("Statement for {0}\n", customerName);
        }

        private static string InvoiceBodyDefault(IEnumerable<Performance> play)
        {
            StringBuilder sb = new();
            CultureInfo cultureInfo = new("en-US");

            foreach (Performance perf in play)
            {
                sb = sb.Append(
                    string.Format(
                        cultureInfo,
                        "  {0}: {1:C} ({2} seats)\n",
                        perf.Name,
                        perf.CalculateTotalCost(),
                        perf.Audience
                    )
                );
            }

            return sb.ToString();
        }

        private static string InvoiceFooterDefault(IEnumerable<Performance> play)
        {
            CultureInfo cultureInfo = new("en-US");
            StringBuilder sb = new();

            sb.Append(
                string.Format(
                    cultureInfo,
                    "Amount owed is {0:C}\n",
                    play.Sum(p => p.CalculateTotalCost())
                )
            );

            sb.Append(
                string.Format(
                    "You earned {0} credits\n",
                    play.Sum(perf => perf.CalculateCredits())
                )
            );

            return sb.ToString();
        }
    }
}
