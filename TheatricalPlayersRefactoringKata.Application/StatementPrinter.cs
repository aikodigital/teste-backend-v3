using System.Globalization;
using TheatricalPlayersRefactoringKata.Application.Models;

namespace TheatricalPlayersRefactoringKata.Application;

public class StatementPrinter
{
    public static string Print(InvoiceModel invoice, Dictionary<string, PlayModel> plays, ReportType reportType = ReportType.TXT)
    {
        double totalAmount = 0;
        int totalVolumeCredits = 0;

        var cultureInfo = new CultureInfo("en-US");

        var resultObject = new StatementModel
        {
            Customer = invoice.Customer,
            Items = new List<Item>()
        };

        foreach (var perf in invoice.Performances)
        {
            CalculateData(plays, perf, out PlayModel play, out double thisAmount, out int thisVolumeCredits);

            var roundThisAmount = Math.Round(thisAmount, 2);

            resultObject.Items.Add(new Item
            {
                PlayName = play.Name,
                EarnedCredits = thisVolumeCredits,
                AmountOwed = roundThisAmount,
                Seats = perf.Audience
            });

            totalAmount += roundThisAmount;
            totalVolumeCredits += thisVolumeCredits;
        }

        resultObject.AmountOwed = Math.Round(totalAmount, 2);
        resultObject.EarnedCredits = totalVolumeCredits;

        string result;

        if (reportType == ReportType.TXT)
            result = resultObject.ToTXT(cultureInfo);
        else
            result = resultObject.ToXML(cultureInfo);

        return result;
    }

    private static void CalculateData(Dictionary<string, PlayModel> plays, PerformanceModel perf, out PlayModel play, out double thisAmount, out int thisVolumeCredits)
    {
        play = plays[perf.PlayId];
        var lines = play.Lines;

        if (lines < 1000)
            lines = 1000;

        if (lines > 4000)
            lines = 4000;

        thisAmount = lines / 10.0;
        thisVolumeCredits = 0;

        thisAmount = play.Type switch
        {
            TypePlay.Tragedy => AmountTragedy(perf, thisAmount),
            TypePlay.Comedy => AmountComedy(perf, thisAmount),
            TypePlay.History => AmountHistory(perf, thisAmount),
            _ => throw new Exception("unknown type: " + play.Type),
        };

        // add volume credits
        thisVolumeCredits += Math.Max(perf.Audience - 30, 0);

        // add extra credit for every ten comedy attendees
        if (play.Type == TypePlay.Comedy)
            thisVolumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
    }

    private static double AmountTragedy(PerformanceModel perf, double thisAmount)
    {
        if (perf.Audience > 30)
        {
            thisAmount += 10.00 * (perf.Audience - 30);
        }

        return thisAmount;
    }

    private static double AmountComedy(PerformanceModel perf, double thisAmount)
    {
        if (perf.Audience > 20)
        {
            thisAmount += 100.00 + 5.00 * (perf.Audience - 20);
        }

        thisAmount += 3.00 * perf.Audience;

        return thisAmount;
    }

    private static double AmountHistory(PerformanceModel perf, double thisAmount)
    {
        return AmountComedy(perf, thisAmount) + AmountTragedy(perf, thisAmount);
    }
}
