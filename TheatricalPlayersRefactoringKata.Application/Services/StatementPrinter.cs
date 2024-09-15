using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Application.Constants;

namespace TheatricalPlayersRefactoringKata.Application;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0m;
        var volumeCredits = 0m;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId];
            var lines = play.Lines;
            if (lines < StatementPrinterConstants.MINIMUM_LINES) lines = StatementPrinterConstants.MINIMUM_LINES;
            if (lines > StatementPrinterConstants.MAXIMUM_LINES) lines = StatementPrinterConstants.MAXIMUM_LINES;
            var thisAmount = lines / StatementPrinterConstants.DIVIDER_PER_LINE;

            switch (play.Type)
            {
                case "tragedy":
                    if (performance.Audience > StatementPrinterConstants.TRAGEDY_MINIMUM_AUDIENCE)
                    {
                        thisAmount += StatementPrinterConstants.TRAGEDY_PER_AUDIENCE_ADDITIONAL 
                                      * (performance.Audience - StatementPrinterConstants.TRAGEDY_MINIMUM_AUDIENCE);
                    }
                    break;
                case "comedy":
                    if (performance.Audience > StatementPrinterConstants.COMEDY_MINIMUM_AUDIENCE)
                    {
                        thisAmount += StatementPrinterConstants.COMEDY_BONUS 
                                      + StatementPrinterConstants.COMEDY_PER_AUDIENCE_ADDITIONAL
                                      * (performance.Audience - StatementPrinterConstants.COMEDY_MINIMUM_AUDIENCE);
                    }
                    thisAmount += StatementPrinterConstants.COMEDY_PER_AUDIENCE * performance.Audience;
                    break;
                case "history":
                    var tragedyAmount = (lines / StatementPrinterConstants.DIVIDER_PER_LINE);
                    if (performance.Audience > StatementPrinterConstants.TRAGEDY_MINIMUM_AUDIENCE)
                    {
                        tragedyAmount += StatementPrinterConstants.TRAGEDY_PER_AUDIENCE_ADDITIONAL
                                      * (performance.Audience - StatementPrinterConstants.TRAGEDY_MINIMUM_AUDIENCE);
                    }

                    var comedyAmount = (lines / StatementPrinterConstants.DIVIDER_PER_LINE);
                    if (performance.Audience > StatementPrinterConstants.COMEDY_MINIMUM_AUDIENCE)
                    {
                        comedyAmount += StatementPrinterConstants.COMEDY_BONUS
                                      + StatementPrinterConstants.COMEDY_PER_AUDIENCE_ADDITIONAL
                                      * (performance.Audience - StatementPrinterConstants.COMEDY_MINIMUM_AUDIENCE);
                    }
                    comedyAmount += StatementPrinterConstants.COMEDY_PER_AUDIENCE * performance.Audience;
                    thisAmount = tragedyAmount + comedyAmount;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            // add volume credits
            volumeCredits += Math.Max(performance.Audience - StatementPrinterConstants.CREDIT_MINIMUM_AUDIENCE, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type) volumeCredits += (int)Math.Floor(performance.Audience / StatementPrinterConstants.COMEDY_BONUS_CREDIT_PER_ATTENDEES);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, performance.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
