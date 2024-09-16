using Newtonsoft.Json;
using System.Globalization;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Linq;

namespace TheatricalPlayersRefactoringKata
{
    public class Converter
    {
        public string ConvertJsonToXml(string json)
        {
            Statement statement = JsonConvert.DeserializeObject<Statement>(json);

            var xmlStatement = new Statement
            {
                Customer = statement.Customer,
                Items = statement.Items.Select(item => new Item
                {
                    AmountOwed = item.AmountOwed,
                    EarnedCredits = item.EarnedCredits,
                    Seats = item.Seats

                }).ToList(),
                AmountOwed = statement.AmountOwed,
                EarnedCredits = statement.EarnedCredits,
            };

            var xmlSettings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = System.Text.Encoding.UTF7
            };

            var xmlSerializer = new XmlSerializer(typeof(Statement));
            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter, xmlSettings))
            {
                xmlSerializer.Serialize(xmlWriter, xmlStatement);
                return stringWriter.ToString();
            }
        }

        public string ConvertJsonToTxt(string json)
        {
            Statement statement = JsonConvert.DeserializeObject<Statement>(json);

            var result = $"Statement for {statement.Customer}\n";
            CultureInfo cultureInfo = new("en-US");

            foreach (var item in statement.Items)
            {
                result += String.Format(cultureInfo, "  {0}: {1} ({2} seats)\n",
                    item.PlayName,
                    item.AmountOwed.ToString("C", cultureInfo),
                    item.Seats);
            }

            result += String.Format(cultureInfo, "Amount owed is {0}\n",
                statement.AmountOwed.ToString("C", cultureInfo));

            result += String.Format("You earned {0} credits\n", statement.EarnedCredits);

            return result;
        }
    }
}
