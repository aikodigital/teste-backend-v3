using System.Globalization;
using System.Xml;
using System.Text;

namespace TheatricalPlayersRefactoringKata.Modules;

public class XMLOutputWritter : AbstractOutputWritter
{
    override public string FileType { get => "xml"; }

    override public byte[] GenerateOutput(Invoice invoice, CultureInfo cultureInfo)
    {
        if (invoice.Results == null)
        {
            throw new Exception("Results not calculated");
        }

        XmlDocument result = new XmlDocument();
        result.AppendChild(result.CreateXmlDeclaration("1.0", "utf-8", null));

        // <Statement />
        XmlElement statement = result.CreateElement("Statement");
        statement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        statement.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
        result.AppendChild(statement);

        // <Customer />
        XmlElement customer = result.CreateElement("Customer");
        customer.InnerText = invoice.Customer;
        statement.AppendChild(customer);

        // <Items />
        XmlElement items = result.CreateElement("Items");
        statement.AppendChild(items);

        /*
            <Item>
                <AmountOwed />
                <EarnedCredits />
                <Seats />
            </Item>
        */
        foreach (Performance performance in invoice.Performances)
        {
            if (performance.Results == null)
            {
                throw new Exception("Results not calculated for performance");
            }

            XmlElement item = result.CreateElement("Item");
            items.AppendChild(item);

            // <AmountOwed />
            XmlElement amountOwed = result.CreateElement("AmountOwed");
            amountOwed.InnerText = performance.Results.AmountOwed.ToString(CultureInfo.InvariantCulture);
            item.AppendChild(amountOwed);

            // <EarnedCredits />
            XmlElement earnedCredits = result.CreateElement("EarnedCredits");
            earnedCredits.InnerText = performance.Results.EarnedCredits.ToString(CultureInfo.InvariantCulture);
            item.AppendChild(earnedCredits);

            // <Seats />
            XmlElement seats = result.CreateElement("Seats");
            seats.InnerText = performance.Audience.ToString();
            item.AppendChild(seats);
        }

        // <AmountOwed />
        XmlElement totalAmountOwed = result.CreateElement("AmountOwed");
        totalAmountOwed.InnerText = invoice.Results.TotalAmountOwed.ToString(CultureInfo.InvariantCulture);
        statement.AppendChild(totalAmountOwed);

        // <EarnedCredits />
        XmlElement totalCreditsEarned = result.CreateElement("EarnedCredits");
        totalCreditsEarned.InnerText = invoice.Results.TotalEarnedCredits.ToString(CultureInfo.InvariantCulture);
        statement.AppendChild(totalCreditsEarned);

        byte[] xmlBytes;
        using (MemoryStream stream = new MemoryStream())
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n"
            };

            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                result.WriteTo(writer);
            }

            xmlBytes = stream.ToArray();
        }

        return xmlBytes;
    }
}