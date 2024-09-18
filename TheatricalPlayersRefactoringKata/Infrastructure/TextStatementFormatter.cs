using System.Globalization;
using TheatricalPlayersRefactoringKata.Data;

namespace TheatricalPlayersRefactoringKata.Infrastructure
{
    public class TextStatementFormatter : IStatementFormatter
    {
        public string Format(StatementData statementData)
        {
            var result = string.Format("Statement for {0}\n", statementData.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var item in statementData.Items)
            {
                result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", item.PlayName, item.AmountOwed, item.Seats);
            }

            result += string.Format(cultureInfo, "Amount owed is {0:C}\n", statementData.TotalAmountOwed);
            result += string.Format("You earned {0} credits\n", statementData.TotalEarnedCredits);
            return result;
        }
    }
}
