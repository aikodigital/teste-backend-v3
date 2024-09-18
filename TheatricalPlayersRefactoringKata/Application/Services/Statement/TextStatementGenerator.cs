using System.Text;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Strategy;

namespace TheatricalPlayersRefactoringKata.Application.Services.Statement
{
    public class TextStatementGenerator : IStatementGeneratorStrategy
    {
        public async Task<string> GenerateStatement(Invoice invoice)
        {
            return await Task.Run(() =>
            {
                StringBuilder sb = new();

                var customerName = invoice.Customer.Name;
                var performances = invoice.Performances.Select(inv => inv.Value);

                sb.Append(InvoiceHeaderDefault(customerName));
                sb.Append(InvoiceBodyDefault(performances));
                sb.Append(InvoiceFooterDefault(performances));

                return sb.ToString();
            });
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
