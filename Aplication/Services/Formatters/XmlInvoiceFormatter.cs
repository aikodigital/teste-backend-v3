using Aplication.DTO;
using Aplication.Interfaces;
using System.Globalization;
using System.Xml.Linq;

namespace Aplication.Services.Formatters
{
    public class XmlInvoiceFormatter : IInvoiceFormatter
    {
        public string Format(InvoiceDto invoice, int valorTotal, int valorCreditos,
            IEnumerable<PerformanceResult> performances)
        {
            CultureInfo cultura = new CultureInfo("en-US");
            var xml = new XElement("Statement",
                new XElement("Customer", invoice.Customer),
                new XElement("Items",
                    performances.Select(perf =>
                    new XElement("Item",
                        new XElement("AmountOwed", 
                            (perf.ValorPorPerformance % 100 == 0) 
                                ? string.Format(cultura, "{0:N0}", Convert.ToDecimal(perf.ValorPorPerformance) / 100) 
                                : string.Format(cultura, "{0:N1}", Convert.ToDecimal(perf.ValorPorPerformance) / 100)),
                        new XElement("EarnedCredits", perf.EarnedCredits),
                        new XElement("Seats", perf.Audience)
                    )
                )),
                new XElement("AmountOwed", performances.Sum(x => Convert.ToDecimal(x.ValorPorPerformance) / 100)),
                new XElement("EarnedCredits", performances.Sum(x => x.EarnedCredits))
            );

            return xml.ToString();
        }
    }
}
