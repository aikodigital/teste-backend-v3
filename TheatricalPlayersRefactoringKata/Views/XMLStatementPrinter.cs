using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Presenters;

namespace TheatricalPlayersRefactoringKata.Views;
public class XMLStatementPrinter: IStatementPrinter
{
    public string Print(StatementPresenter statement)
    {   
        XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

        XDocument doc = new XDocument(
            new XDeclaration("1.0", "utf-8", string.Empty),
            new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(XNamespace.Xmlns + "xsd", xsd),
                new XElement("Customer", statement.Customer),
                new XElement("Items",
                    PerfomancesElements(statement.Perfomances)
                ),
                new XElement("AmountOwed", statement.TotalCharge),
                new XElement("EarnedCredits", statement.TotalCredits)
            )
        );

        return XDocumentToString(doc);
    }


    // Note: This method of getting the string for the xml is used
    // as calling ToString in document will not contain the xml declaration
    // and writing to a StringWriter will override the utf-8 to utf-16.
    private string XDocumentToString(XDocument doc) {
        using MemoryStream stream = new MemoryStream();
        using StreamReader reader = new StreamReader(stream);
        doc.Save(stream);
        stream.Seek(0, SeekOrigin.Begin);
        return reader.ReadToEnd();
    }

    private List<XElement> PerfomancesElements(List<StatementPerfomance> perfomances) {
        return perfomances.ConvertAll(perfomance => 
            new XElement("Item",
                new XElement("AmountOwed", perfomance.Charge),
                new XElement("EarnedCredits", perfomance.Credits),
                new XElement("Seats", perfomance.Audience)
            )
        );
    }
}
