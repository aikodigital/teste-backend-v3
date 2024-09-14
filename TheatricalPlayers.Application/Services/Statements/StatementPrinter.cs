using System.Globalization;
using TheatricalPlayers.Application.Strategies;
using TheatricalPlayers.Core.Entities;
using TheatricalPlayers.Core.Interfaces.Statements;
using TheatricalPlayers.Core.Interfaces.Strategies;

namespace TheatricalPlayers.Application.Services.Statements
{
    public class StatementPrinter : IStatementPrinter // gerador de extratos
    {
        private readonly CultureInfo _cultureInfo;

        public StatementPrinter()
        {
            _cultureInfo = new CultureInfo("en-US");
        }

        public string Print(Invoice invoice, List<Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = $"Statement for {invoice.Customer}\n";

            foreach (var performance in invoice.Performances)
            {
                var play = plays.First(play => play.Id == performance.PlayId);
                
                var playTypeStrategy = StrategyFactory.GetStrategy(play.Type);
                
                var amount = playTypeStrategy.CalculateAmount(play.Lines, performance.Audience);

                volumeCredits += playTypeStrategy.CalculateVolumeCredits(performance.Audience);

                result += FormatPerformanceLine(play.Name, amount, performance.Audience);
                totalAmount += amount;
            }

            result += FormatTotalAmount(totalAmount);
            result += $"You earned {volumeCredits} credits\n";
            return result;
        }
        
        private string FormatPerformanceLine(string playName, int amount, int audience)
        {
            return string.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", playName, Convert.ToDecimal(amount / 100), audience);
        }

        private string FormatTotalAmount(int totalAmount)
        {
            return string.Format(_cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        }
    }
}
