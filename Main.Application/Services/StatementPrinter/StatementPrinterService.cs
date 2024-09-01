using Main.Contracts.StatementPrinter;
using System.Globalization;
using Main.Domain.Services;
using System.Numerics;

namespace Main.Application.Services.StatementPrinter
{
    public class StatementPrinterService : IStatementPrinterService
    {
        private readonly StatementCalculator _calculator;
        private readonly StatementFormatter _formatter;

        public StatementPrinterService(StatementCalculator calculator, StatementFormatter formatter)
        {
            _calculator = calculator;
            _formatter = formatter;
        }

        public StatementPrinterResult Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var result = _formatter.EntryStatement(invoice.Customer);
            var totalAmount = 0;
            var volumeCredits = 0;
            CultureInfo cultureInfo = new CultureInfo("en-US");

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

                volumeCredits = _calculator.CalculateVolumeCredits(volumeCredits,play.Type,audience);
                result += _formatter.FormatStatement(play.Name, thisAmount, perf.Audience, cultureInfo);
                totalAmount += thisAmount;
            }
            result += _formatter.FinalStatement(totalAmount, volumeCredits, cultureInfo);
            return new StatementPrinterResult(result);
        }
    }
}
