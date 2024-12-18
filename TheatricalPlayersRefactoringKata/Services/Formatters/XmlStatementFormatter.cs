using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services.Formatters
{
    public class XmlStatementFormatter : IStatementFormatter
    {
        private static readonly XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        private static readonly XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

        public string Format(Statement statement)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = false,
                Encoding = new UTF8Encoding(true), 
                CloseOutput = true
            };

            var ms = new MemoryStream();
            using (var writer = XmlWriter.Create(ms, settings))
            {
                var doc = new XDocument(
                    new XElement("Statement",
                        new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                        new XAttribute(XNamespace.Xmlns + "xsd", xsd),
                        new XElement("Customer", statement.Customer),
                        new XElement("Items",
                            statement.Items.Select(item =>
                                new XElement("Item",
                                    new XElement("AmountOwed", FormatAmount(item.AmountOwed)),
                                    new XElement("EarnedCredits", item.EarnedCredits),
                                    new XElement("Seats", item.Seats)
                                )
                            )
                        ),
                        new XElement("AmountOwed", FormatAmount(statement.TotalAmount)),
                        new XElement("EarnedCredits", statement.TotalCredits)
                    )
                );

                doc.Save(writer);
            }

            var xmlString = Encoding.UTF8.GetString(ms.ToArray());
            return xmlString;
        }

        private string FormatAmount(decimal amount)
        {
            return amount % 1 == 0 
                ? ((int)amount).ToString() 
                : amount.ToString("0.0", CultureInfo.InvariantCulture);
        }
    }
}
