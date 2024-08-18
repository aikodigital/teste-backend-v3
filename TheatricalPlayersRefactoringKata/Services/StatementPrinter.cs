using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class StatementPrinter
    {
        /// <summary>
        /// Print the statement in text format
        /// </summary>
        /// <returns> Text string </returns>
        public static string TxtPrint(Statement statement)
        {
            var result = new StringBuilder();
            CultureInfo cultureInfo = new CultureInfo("en-US");
            result.AppendFormat("Statement for {0}\n", statement.Customer);
            foreach (StatementLine line in statement.Lines)
            {
                result.AppendFormat(cultureInfo, "  {0}: {1:C} ({2} seats)\n", line.Name, line.Amount, line.Seats);
            }
            result.AppendFormat(cultureInfo, "Amount owed is {0:C}\n", statement.TotalAmount);
            result.AppendFormat("You earned {0} credits\n", statement.VolumeCredits);
            return result.ToString();
        }

        /// <summary>
        /// Print the statement in XML format
        /// </summary>
        /// <returns> XML string </returns>
        public static string XmlPrint(Statement statement)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };
            // fix UTF-8 encoding
            using var memoryStream = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(memoryStream, settings))
            {
                // create root element
                writer.WriteStartElement("Statement");
                // add xmlns:xsi
                writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                // add xmlns:xsd
                writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
                // add customer element
                writer.WriteElementString("Customer", statement.Customer);
                // add elements for each line
                writer.WriteStartElement("Items");
                foreach (var line in statement.Lines)
                {
                    writer.WriteStartElement("Item");
                    writer.WriteElementString("AmountOwed", line.Amount.ToString(CultureInfo.InvariantCulture));
                    writer.WriteElementString("EarnedCredits", line.Credits.ToString());
                    writer.WriteElementString("Seats", line.Seats.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                // add AmountOwed element
                writer.WriteElementString("AmountOwed", statement.TotalAmount.ToString(CultureInfo.InvariantCulture));
                // add EarnedCredits element
                writer.WriteElementString("EarnedCredits", statement.VolumeCredits.ToString());
                // close
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            // Convert the MemoryStream to a UTF-8 encoded string
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}