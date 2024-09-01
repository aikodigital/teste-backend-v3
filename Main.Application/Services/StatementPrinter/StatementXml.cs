using System.Text;
using System.Xml.Serialization;
using Main.Contracts.StatementPrinter;
using Main.Domain.Services;

namespace Main.Application.Services.StatementPrinter
{
    public class XmlStatementPrinterService : IStatementPrinterService
    {
        private readonly StatementCalculator _calculator;

        public XmlStatementPrinterService(StatementCalculator calculator)
        {
            _calculator = calculator;
        }

        public StatementPrinterResult Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var statement = new Statement
            {
                Customer = invoice.Customer,
                Items = new List<Item>(),
                AmountOwed = 0,
                EarnedCredits = 0
            };

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var lines = _calculator.AdjustLines(play.Lines);
                var audience = perf.Audience;
                var thisAmount = lines * 10;

                switch (play.Type)
                {
                    case "tragedy":
                        thisAmount = _calculator.CalculateTragedyAmount(thisAmount, audience);
                        break;
                    case "comedy":
                        thisAmount = _calculator.CalculateComedyAmount(thisAmount, audience);
                        break;
                    case "history":
                        thisAmount = _calculator.CalculateHistoryAmount(thisAmount, audience);
                        break;
                    default:
                        throw new Exception("unknown type: " + play.Type);
                }

                var credits = _calculator.CalculateVolumeCredits(0, play.Type, audience);
                statement.Items.Add(new Item
                {
                    AmountOwed = Convert.ToDecimal(thisAmount)/100,
                    EarnedCredits = credits,
                    Seats = audience
                });

                statement.AmountOwed += Convert.ToDecimal(thisAmount) / 100;
                statement.EarnedCredits += credits;
            }

            var xmlSerializer = new XmlSerializer(typeof(Statement));
            var xmlResult = string.Empty;

            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
                {
                    xmlSerializer.Serialize(streamWriter, statement);
                    streamWriter.Flush();
                    memoryStream.Position = 0;

                    using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
                    {
                        xmlResult = reader.ReadToEnd();
                    }
                }
            }
            return new StatementPrinterResult(xmlResult);
        }
    }
}
