using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementService
    {
        private readonly Dictionary<string, IPlayTypeService> _playTypeServices;

        public StatementService(Dictionary<string, IPlayTypeService> playTypeServices)
        {
            _playTypeServices = playTypeServices;
        }

        public string GenerateStatement(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = $"Statement for {invoice.Customer}\n";
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var playTypeService = _playTypeServices[play.Type];

                var thisAmount = playTypeService.CalculateAmount(perf, play);
                volumeCredits += playTypeService.CalculateVolumeCredits(perf);

                // print line for this order
                result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
                totalAmount += thisAmount;
            }

            result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += $"You earned {volumeCredits} credits\n";

            return result;
        }
    }
}
