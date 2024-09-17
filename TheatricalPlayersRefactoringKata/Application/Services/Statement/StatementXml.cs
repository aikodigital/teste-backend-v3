using System.Linq;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Strategy;

namespace TheatricalPlayersRefactoringKata.Application.Services.Statement
{
    public class StatementXml : IStatement
    {
        public string Print(Invoice invoice)
        {
            var perfomances = invoice.Performances.Values;

            var itemElement = new XElement("Item");
            foreach (var perf in perfomances)
            {
                itemElement.Add(
                    new XElement("AmountOwed", perf.CalculateTotalCost()),
                    new XElement("EarnedCredits", perf.CalculateCredits()),
                    new XElement("Seats", perf.Audience)
                );
            }

            var customerName = invoice.Customer.Name;

            XDocument xDocument = new(
                new XElement("Statement", "xmlns: xsi = 'http://www.w3.org/2001/XMLSchema-instance' xmlns: xsd = 'http://www.w3.org/2001/XMLSchema'"),
                new XElement("Customer", customerName),
                new XElement("Items"),
                itemElement,
                new XElement("AmountOwed", perfomances.Sum(perf => perf.CalculateTotalCost())),
                new XElement("EarnedCredits", perfomances.Sum(perf => perf.CalculateCredits()))
            );

            string filePath = "C:\\Users\\jheik\\Downloads\\Teste\\teste-jheik-alves\\TheatricalPlayersRefactoringKata.Tests\\TesteXML.xml";
            xDocument.Save(filePath);

            return xDocument.ToString();
        }
    }
}
