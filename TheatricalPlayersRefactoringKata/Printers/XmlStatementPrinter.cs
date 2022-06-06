using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.General;

namespace TheatricalPlayersRefactoringKata.Printers;

public class XmlStatementPrinter : IStatementPrinter
{
    public string Print(Statement statement, CultureInfo cultureInfo = null)
    {
        var serializer = new XmlSerializer(typeof(XmlStatement));
        var xmlStatement = new XmlStatement(statement, cultureInfo);
        
        using var stream = new Utf8StringWriter();
        var xmlWriter = XmlWriter.Create(stream, new XmlWriterSettings
        {
            Encoding = Encoding.UTF8,
            Indent = true
        });
        serializer.Serialize(xmlWriter, xmlStatement);
        
        return stream.ToString();
    }

    [XmlRoot("Statement")]
    public struct XmlStatement
    {
        const string DecimalFormat = "########0.#########";

        public string Customer;
        [XmlArray("Items"), XmlArrayItem("Item")]
        public List<XmlStatementItem> Items;
        public string AmountOwed;
        public int EarnedCredits;

        public XmlStatement(Statement statement, CultureInfo cultureInfo)
        {
            Customer = statement.Customer;
            AmountOwed = statement.TotalAmount.ToString(DecimalFormat, cultureInfo);
            EarnedCredits = statement.TotalCredits;

            Items = new List<XmlStatementItem>();

            foreach(var (perf, entry) in statement)
            {
                var item = new XmlStatementItem
                {
                    AmountOwed = entry.Amount.ToString(DecimalFormat, cultureInfo),
                    EarnedCredits = entry.Credits,
                    Seats = perf.Audience
                };

                Items.Add(item);
            }
        }
    }
    public struct XmlStatementItem
    {
        public string AmountOwed;
        public int EarnedCredits;
        public int Seats;
    }
}