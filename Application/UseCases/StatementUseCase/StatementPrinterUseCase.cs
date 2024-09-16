using Domain.Contracts.UseCases.StatementUseCase;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata;

namespace Application.UseCases.StatementUseCase
{
    public class StatementPrinterUseCase : IStatementPrinterUseCase
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var statement = new Statement
            {
                Customer = invoice.Customer,
                Items = new List<Item>()
            };

            decimal totalAmount = 0;
            decimal volumeCredits = 0;

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];

                decimal lines = play.Lines;
                lines = Math.Max(1000, Math.Min(lines, 4000));

                decimal thisAmount = lines * 10;
                int earnedCredits = 0;

                switch (play.Type)
                {
                    case "tragedy":
                        if (perf.Audience > 30)
                        {
                            thisAmount += 1000 * (perf.Audience - 30);
                        }
                        break;
                    case "comedy":
                        if (perf.Audience > 20)
                        {
                            thisAmount += 10000 + 500 * (perf.Audience - 20);
                        }
                        thisAmount += 300 * perf.Audience;
                        break;
                    case "history":
                        decimal tragedyAmount = thisAmount;
                        decimal comedyAmount = thisAmount + 300 * perf.Audience;

                        if (perf.Audience > 30)
                        {
                            tragedyAmount += 1000 * (perf.Audience - 30);
                        }
                        if (perf.Audience > 20)
                        {
                            comedyAmount += 10000 + 500 * (perf.Audience - 20);
                        }

                        thisAmount = tragedyAmount + comedyAmount;
                        break;
                    default:
                        throw new Exception("unknown type: " + play.Type);
                }

                // Add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);

                // Add extra credit for every ten comedy attendees
                if ("comedy" == play.Type)
                {
                    volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
                    earnedCredits = Math.Max(perf.Audience - 30, 0);
                    earnedCredits += (int)Math.Floor((decimal)perf.Audience / 5);
                }
                else
                    earnedCredits = Math.Max(perf.Audience - 30, 0);

                // Add item to the statement
                statement.Items.Add(new Item
                {
                    PlayName = play.Name,
                    AmountOwed = Convert.ToDecimal(thisAmount / 100),
                    Seats = perf.Audience,
                    EarnedCredits = earnedCredits
                });

                totalAmount += thisAmount;
            }

            statement.AmountOwed = Convert.ToDecimal(totalAmount / 100);
            statement.EarnedCredits = (int)volumeCredits;

            return JsonConvert.SerializeObject(statement, Newtonsoft.Json.Formatting.Indented);
        }

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
                result += string.Format(cultureInfo, "  {0}: {1} ({2} seats)\n",
                    item.PlayName,
                    item.AmountOwed.ToString("C", cultureInfo),
                    item.Seats);
            }

            result += string.Format(cultureInfo, "Amount owed is {0}\n",
                statement.AmountOwed.ToString("C", cultureInfo));

            result += string.Format("You earned {0} credits\n", statement.EarnedCredits);

            return result;
        }
        private string FormatAmount(decimal amount)
        {
            return amount % 1 == 0 ? amount.ToString("F0") : amount.ToString("F1");
        }
    }
}
