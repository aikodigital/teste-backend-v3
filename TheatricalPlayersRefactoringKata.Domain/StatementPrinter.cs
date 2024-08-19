using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Application.Enums;
using TheatricalPlayersRefactoringKata.Domain.Common;
using TheatricalPlayersRefactoringKata.Domain.Core;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter {

    public static readonly Dictionary<Enum, IPlayGenre> _genres = new() {
            {  EnumPlayGenre.Tragedy, new TragedyGenre() },
            {  EnumPlayGenre.Comedy, new ComedyGenre()  },
            {  EnumPlayGenre.Historical, new HistoricalGenre() },
        };

    public string Print(Invoice invoice, Dictionary<string, Play> plays) {
        double totalAmount = 0;
        double volumeCredits = 0;
        string result = string.Format("Statement for {0}\n", invoice.Customer);

        foreach (var perf in invoice.Performances) {
            Play play = plays[perf.PlayId];

            double thisAmount = AppConstants.CalculatePlayLines(perf, play);

            IPlayGenre genre = _genres[play.Type];

            thisAmount = genre.CalculatePlayAmount(perf);
            volumeCredits += genre.CalculatePlayCredits(perf);

            // print line for this order
            result += string.Format(AppConstants.cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;
        }
        result += string.Format(AppConstants.cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += string.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
