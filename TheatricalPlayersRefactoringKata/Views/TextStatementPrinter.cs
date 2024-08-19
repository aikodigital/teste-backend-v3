using System;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Presenters;

namespace TheatricalPlayersRefactoringKata.Views;
public class TextStatementPrinter: IStatementPrinter
{
    public string Print(StatementPresenter statement)
    {
        var result = string.Format("Statement for {0}\n", statement.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(StatementPerfomance perf in statement.Perfomances) 
        {
            decimal perfomanceCharge = perf.Charge;

            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", perf.PlayName, perf.Charge, perf.Audience);
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", statement.TotalCharge);
        result += String.Format("You earned {0} credits\n", statement.TotalCredits);
        return result;
    }
}
