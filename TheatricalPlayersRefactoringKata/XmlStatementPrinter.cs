using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace TheatricalPlayersRefactoringKata
{
public interface IStatementPrinter
{
    string Print(Invoice invoice);
}

public class XmlStatementPrinter : IStatementPrinter
{
    public string Print(Invoice invoice)
    {
        var statement = new XElement("Statement",
            new XElement("Customer", invoice.Customer),
            new XElement("Items",
                from performance in invoice.Performances
                select new XElement("Item",
                    new XElement("AmountOwed", performance.CalculateAmount() / 100),
                    new XElement("EarnedCredits", performance.CalculateCredits()),
                    new XElement("Seats", performance.Audience))),
            new XElement("AmountOwed", invoice.TotalAmount() / 100),
            new XElement("EarnedCredits", invoice.TotalCredits()));

        return statement.ToString();
    }
}

}