using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.TextFileGenerator
{
    public class TextFileGeneratorText : ITextFileGenerator
    {
        public string TextFile(Invoice invoice, List<Statement> statements)
        {
            // generates the text file of stataments
            decimal totalAmount = 0;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var stat in statements)
            {
                // print line for this order
                result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", stat.Customer, Convert.ToDecimal(stat.AmountOwed / 100), stat.Seats);
                totalAmount += stat.AmountOwed;
                volumeCredits += stat.EarnedCredits;
            }

            result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += string.Format("You earned {0} credits\n", volumeCredits);

            return result;
        }
    }
}
