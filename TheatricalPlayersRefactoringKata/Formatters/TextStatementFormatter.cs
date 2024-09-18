using System;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Formatters.Interface;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Formatters;

public class TextStatementFormatter : IExtratoFormatter
{
    public string FormatCustomer(string customer)
    {
        return string.Format("Statement for {0}\n", customer);
    }
    public string FormatPerformance(Play play, Performance performance, decimal performanceAmount, decimal performaceCredits, CultureInfo cultureInfo)
    {
        return string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(performanceAmount / 100), performance.Audience);
    }

    public string FormatTotalAmount(decimal totalAmount, CultureInfo cultureInfo)
    {
        return string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
    }

    public string FormatTotalCredits(int totalCredits)
    {
        return string.Format("You earned {0} credits\n", totalCredits);
    }

    public string GenerateStatement(string customer, string performace, string totalAmount, string totalCredits, CultureInfo cultureInfo)
    {
        string result = ($"{customer}{performace}{totalAmount}{totalCredits}");
        return result;
    }
}

