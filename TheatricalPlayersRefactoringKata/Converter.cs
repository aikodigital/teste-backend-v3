using Newtonsoft.Json;
using System.Globalization;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Linq;
using System.Text;

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
                    AmountOwed = decimal.Parse(FormatAmount(item.AmountOwed)),
                    EarnedCredits = item.EarnedCredits,
                    Seats = item.Seats

                }).ToList(),
                AmountOwed = decimal.Parse(FormatAmount(statement.AmountOwed)),
                EarnedCredits = statement.EarnedCredits,
            };

            var xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true
            };

            var xmlSerializer = new XmlSerializer(typeof(Statement));
            using var memoryStream = new MemoryStream();
            using var xmlWriter = XmlWriter.Create(memoryStream, xmlSettings);
            xmlSerializer.Serialize(xmlWriter, xmlStatement);
            string xmlOutput = Encoding.UTF8.GetString(memoryStream.ToArray());

            return xmlOutput.ToString();
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
        private string FormatAmount(decimal amount)
        {
            return amount % 1 == 0 ? amount.ToString("F0") : amount.ToString("F1");
        }
    }
}
