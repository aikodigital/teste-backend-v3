using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Factory;
using TheatricalPlayersRefactoringKata.Interface;
using TheatricalPlayersRefactoringKata.Model;

namespace TheatricalPlayersRefactoringKata.Formatter;
public class TextStatementFormatter : IStatementFormatter
{
    public string FormatAsync(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0m;
        int volumeCredits = 0;
        string result = $"Statement for {invoice.Customer}\n";
        var cultureInfo = new CultureInfo("en-US");

        foreach (Performance perf in invoice.Performances)
        {
            Play play = plays[perf.PlayId];
            IPlayCalculator calculator = PlayCalculatorFactory.GetCalculator(play.Type);
            decimal thisAmount = calculator.CalculateAmount(perf, play);
            volumeCredits += calculator.CalculateVolumeCredits(perf);

            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount / 100, perf.Audience);
            totalAmount += thisAmount;
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100);
        result += $"You earned {volumeCredits} credits\n";
        return result;
    }

    Task<string> IStatementFormatter.FormatAsync(Invoice invoice, Dictionary<string, Play> plays) => throw new System.NotImplementedException();
}

