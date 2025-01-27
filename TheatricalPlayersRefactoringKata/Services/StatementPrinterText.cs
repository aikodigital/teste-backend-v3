using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.DTO;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Domain.Services
{
    public class StatementPrinterText : IStatementFormatter
    {
        public string Print(StatementDTO statement)
        {
            var cultureInfo = new CultureInfo("en-US");
            var result = string.Format("Statement for {0}\n", statement.Customer);

            foreach (var item in statement.Items)
                result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", item.ItemName, Convert.ToDouble(item.AmountOwed), item.Seats);

            result += string.Format(cultureInfo, "Amount owed is {0:C}\n", statement.AmountOwed);
            result += string.Format("You earned {0} credits\n", statement.EarnedCredits);

            return result;
        }

    }
}
