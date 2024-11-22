using Aplication.DTO;
using Aplication.Interfaces;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Aplication.Services.Formatters
{
    public class XmlInvoiceFormatter : IInvoiceFormatter
    {
        public string Format(InvoiceDto invoice, int valorTotal, int valorCreditos,
            IEnumerable<PerformanceResult> performances)
        {
            CultureInfo cultura = new CultureInfo("en-US");

            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

            var xml = new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(XNamespace.Xmlns + "xsd", xsd),
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

            var xmlDocument = new XDocument
            (
                new XDeclaration("1.0", "utf-8", ""),
                xml
            );
            using Utf8StringWriter stringWriter = SetDefinitions(xmlDocument);
            return stringWriter.ToString();
        }

        private static Utf8StringWriter SetDefinitions(XDocument xmlDocument)
        {
            var stringWriter = new Utf8StringWriter();
            using (var xmlWritter = XmlWriter.Create(stringWriter, new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                OmitXmlDeclaration = false
            }))
            {
                xmlDocument.WriteTo(xmlWritter);
            }

            return stringWriter;
        }
    }
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
