using System.Globalization;
using TheatricalPlayers.Application.Factories;
using TheatricalPlayers.Core.Entities;
using TheatricalPlayers.Core.Interfaces.Statements;

namespace TheatricalPlayers.Application.Handlers
{
    public class StatementPrinterHandler : IStatementPrinterHandler // gerador de extratos
    {
        private readonly CultureInfo _cultureInfo;

        public StatementPrinterHandler()
        {
            _cultureInfo = new CultureInfo("en-US");
        }

        public string Print(Invoice invoice, List<Play> plays)
        {
            var totalPriceCents = 0;
            var volumeCredits = 0;
            var result = $"Statement for {invoice.Customer}\n";

            foreach (var performance in invoice.Performances)
            {
                var play = plays.First(play => play.Id == performance.PlayId);
                
                var playTypeHandler = PlayTypeFactory.GetHandler(play.Type);
                
                var priceCents = playTypeHandler.CalculatePriceCents(play.Lines, performance.Audience);

                volumeCredits += playTypeHandler.CalculateCredits(performance.Audience);

                result += FormatPerformanceLine(play.Name, priceCents, performance.Audience);
                
                totalPriceCents += priceCents;
            }

            result += FormatTotalPrice(totalPriceCents);
            result += $"You earned {volumeCredits} credits\n";
            return result;
        }
        
        private string FormatPerformanceLine(string playName, int priceCents, int audience)
        {
            return string.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", playName, Convert.ToDecimal(priceCents / 100), audience);
        }

        private string FormatTotalPrice(int totalPrice)
        {
            return string.Format(_cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalPrice / 100));
        }
    }
}
