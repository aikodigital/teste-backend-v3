using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Formatters
{
    public class ExtractTextFormatter : IExtractFormatter
    {
        //Generate statement in format Text
        public string Formatter(Invoice invoice, Dictionary<string, Play> plays)
        {
            double totalAmount = 0;
            double volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];

                //Calculate the total value of the audience
                double thisAmount = play.Calculate(perf.Audience);
                totalAmount += thisAmount;

                // Add volume credits
                volumeCredits += play.VolumeCredits(perf.Audience);
                //Formatter Text
                result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount), perf.Audience);
            }
            //Final result
            result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount));
            result += string.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }
    }
}
