using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TheatricalPlayers.Application.Factories;
using TheatricalPlayers.Core.DataTransferObjects.StatementDTOs;
using TheatricalPlayers.Core.Interfaces.Statements;
using TheatricalPlayers.Core.Models;

namespace TheatricalPlayers.Application.Handlers
{
    public class StatementPrinterHandler : IStatementPrinterHandler
    {
        private readonly CultureInfo _cultureInfo;
        private readonly XmlWriterSettings _xmlWriterSettings;

        public StatementPrinterHandler()
        {
            _xmlWriterSettings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                NewLineChars = "\n",
                OmitXmlDeclaration = false
            };
            
            _cultureInfo = new CultureInfo("en-US");
        }
        
        public string PrintTxt(Invoice invoice, List<Play> plays)
        {
            var statement = GetStatement(invoice, plays);
            
            var result = $"Statement for {invoice.Customer}\n";

            foreach (var play in statement.Plays)
                result += FormatPerformanceLine(play.PlayName, play.AmountOwed, play.Seats);

            result += FormatTotalPrice(statement.AmountOwed);
            result += $"You earned {statement.EarnedCredits} credits\n";
            return result;
        }
        
        public string PrintXml(Invoice invoice, List<Play> plays)
        {
            var statement = GetStatement(invoice, plays);

            var serializer = new XmlSerializer(typeof(Statement));

            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream, _xmlWriterSettings.Encoding))
                {
                    using (var xmlWriter = XmlWriter.Create(streamWriter, _xmlWriterSettings))
                    {
                        serializer.Serialize(xmlWriter, statement);
                        streamWriter.Flush();
                        memoryStream.Position = 0;

                        using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public Statement GetStatement(Invoice invoice, List<Play> plays)
        {
            var totalPriceCents = 0;
            var totalCredits = 0;
            
            var statement = new Statement();
            statement.Customer = invoice.Customer;

            foreach (var performance in invoice.Performances)
            {
                var play = plays.First(play => play.Id == performance.PlayId);
                
                var playTypeHandler = PlayTypeFactory.GetHandler(play.Type);
                
                var priceCents = playTypeHandler.CalculatePriceCents(play.Lines, performance.Audience);
                
                var credits = playTypeHandler.CalculateCredits(performance.Audience);
                
                statement.Plays.Add(new StatementPlay
                {
                    PlayName = play.Name,
                    EarnedCredits = credits,
                    Seats = performance.Audience,
                    AmountOwed = (decimal)priceCents / 100
                });
                
                totalCredits += credits;
                totalPriceCents += priceCents;
            }

            statement.EarnedCredits = totalCredits;
            statement.AmountOwed = (decimal)totalPriceCents / 100;
            
            return statement;
        }
        
        private string FormatPerformanceLine(string playName, decimal priceCents, int audience)
        {
            return string.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", playName, priceCents, audience);
        }

        private string FormatTotalPrice(decimal totalPrice)
        {
            return string.Format(_cultureInfo, "Amount owed is {0:C}\n", totalPrice);
        }
    }
}
