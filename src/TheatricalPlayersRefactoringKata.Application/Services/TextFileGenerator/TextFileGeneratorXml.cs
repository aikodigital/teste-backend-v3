using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.TextFileGenerator
{
    public class TextFileGeneratorXml : ITextFileGenerator
    {
        public string TextFile(Invoice invoice, List<Statement> statements)
        {
            decimal amountOwed = statements.Sum(stat => stat.amount);
            decimal earnedCredits = statements.Sum(stat => stat.credit);

            XDeclaration declaration = new XDeclaration("1.0", "utf-8", null);
            XElement xmlStatement = new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", invoice.Customer),
                new XElement("Items",
                    from stat in statements
                    select new XElement("Item", new XElement("AmountOwed", Convert.ToDecimal(stat.amount / 100)), new XElement("EarnedCredits", stat.credit), new XElement("Seats", stat.seat))),
                new XElement("AmountOwed", Convert.ToDecimal(amountOwed / 100)), new XElement("EarnedCredits", earnedCredits));

            string xmlText = declaration.ToString() + Environment.NewLine + xmlStatement.ToString();
            xmlText = "\uFEFF" + xmlText;

            return xmlText;
        }
    }
}
