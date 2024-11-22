using Aplication.DTO;
using Aplication.Interfaces;
using System.Xml.Linq;

namespace Aplication.Services.Formatters
{
    public class XmlInvoiceFormatter : IInvoiceFormatter
    {
        public string Format(InvoiceDto invoice, int valorTotal, int valorCreditos,
            IEnumerable<PerformanceResult> performances)
        {
            var xml = new XElement("Statement",
                new XElement("Customer", invoice.Customer),
                new XElement("Items",
                    performances.Select(perf =>
                    new XElement("Item",
                        new XElement("AmountOwed", perf.ValorPorPerformance),
                        new XElement("EarnedCredits", perf.EarnedCredits),
                        new XElement("Seats", perf.Audience)
                    )
                ),
                new XElement("AmountOwed", performances.Sum(x => x.ValorPorPerformance),
                new XElement("EarnedCredits", performances.Sum(x => x.EarnedCredits))
            )));

            return xml.ToString();
        }
    }
}
