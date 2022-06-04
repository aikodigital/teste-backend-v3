using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.CrossCutting.Extension;
using TheatricalPlayersRefactoringKata.Domain.Interface.Services;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;

namespace TheatricalPlayersRefactoringKata.Domain.Service
{
    public class ExtractXMLService : ExtractService, IExtractXMLService
    {
        public ExtractXMLService(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public override string GenerateExtract(Invoice invoice)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", ""));
            XElement statement = new XElement("Statement", new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance")
                                                         , new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"));
            XElement customer = new XElement("Customer", invoice.Customer.Name);
            XElement amount = new XElement("AmountOwed", ((int)invoice.TotalAmount).ToString());
            XElement credits = new XElement("EarnedCredits", invoice.TotalCredits);
            XElement items = new XElement("Items");
            foreach (Performance performance in invoice.Performances)
            {
                XElement item = new XElement("Item",
                                             new XElement("AmountOwed", ((int)performance.AmountOwed).ToString()),
                                             new XElement("EarnedCredits", performance.EarnedCredits),
                                             new XElement("Seats", performance.Audience));
                items.Add(item);
            }

            statement.Add(customer);
            statement.Add(items);
            statement.Add(amount);
            statement.Add(credits);

            doc.Add(statement);

            StringBuilder builder = new StringBuilder();
            using (TextWriter writer = new StringWriterWithEncoding(Encoding.UTF8, builder))
            {
                doc.Save(writer);
            }

            string result = builder.ToString();
            return result;
        }
    }
}