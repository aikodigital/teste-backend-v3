using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Application.Constants;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Services;
using System.Reflection;

namespace TheatricalPlayersRefactoringKata.Application;

public class StatementPrinter
{
    private readonly ICalculateBaseAmountPerLine _calculateBaseAmountPerLine;
    private readonly ICalculateCreditAudience _calculateCreditAudience;
    private readonly ICalculateAdditionalValuePerGender _calculateAdditionalValuePerGender;

    public StatementPrinter(ICalculateBaseAmountPerLine calculateBaseAmountPerLine,
                            ICalculateCreditAudience calculateCreditAudience,
                            ICalculateAdditionalValuePerGender calculateAdditionalValuePerGender)
    {
        _calculateBaseAmountPerLine = calculateBaseAmountPerLine;
        _calculateCreditAudience = calculateCreditAudience;
        _calculateAdditionalValuePerGender = calculateAdditionalValuePerGender;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0m;
        var volumeCredits = 0m;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId];
            var thisAmount = _calculateBaseAmountPerLine.CalculateBaseAmount(play.Lines);

            switch (play.Gender)
            {
                case "tragedy":
                    thisAmount += _calculateAdditionalValuePerGender.CalculateAdditionalValue(performance.Audience,
                                                                                              StatementPrinterConstants.TRAGEDY_MINIMUM_AUDIENCE,
                                                                                              StatementPrinterConstants.TRAGEDY_BONUS,
                                                                                              StatementPrinterConstants.TRAGEDY_PER_AUDIENCE_ADDITIONAL,
                                                                                              StatementPrinterConstants.TRAGEDY_PER_AUDIENCE);
                    break;
                case "comedy":
                    thisAmount += _calculateAdditionalValuePerGender.CalculateAdditionalValue(performance.Audience,
                                                                                              StatementPrinterConstants.COMEDY_MINIMUM_AUDIENCE,
                                                                                              StatementPrinterConstants.COMEDY_BONUS,
                                                                                              StatementPrinterConstants.COMEDY_PER_AUDIENCE_ADDITIONAL,
                                                                                              StatementPrinterConstants.COMEDY_PER_AUDIENCE);
                    break;
                case "history":
                    var tragedyAmount = _calculateBaseAmountPerLine.CalculateBaseAmount(play.Lines);
                    tragedyAmount += _calculateAdditionalValuePerGender.CalculateAdditionalValue(performance.Audience,
                                                                                                 StatementPrinterConstants.TRAGEDY_MINIMUM_AUDIENCE,
                                                                                                 StatementPrinterConstants.TRAGEDY_BONUS,
                                                                                                 StatementPrinterConstants.TRAGEDY_PER_AUDIENCE_ADDITIONAL,
                                                                                                 StatementPrinterConstants.TRAGEDY_PER_AUDIENCE);

                    var comedyAmount = _calculateBaseAmountPerLine.CalculateBaseAmount(play.Lines);
                    comedyAmount += _calculateAdditionalValuePerGender.CalculateAdditionalValue(performance.Audience,
                                                                                                StatementPrinterConstants.COMEDY_MINIMUM_AUDIENCE,
                                                                                                StatementPrinterConstants.COMEDY_BONUS,
                                                                                                StatementPrinterConstants.COMEDY_PER_AUDIENCE_ADDITIONAL,
                                                                                                StatementPrinterConstants.COMEDY_PER_AUDIENCE);
                    thisAmount = tragedyAmount + comedyAmount;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Gender);
            }

            volumeCredits += _calculateCreditAudience.CalculateCredit(performance.Audience, play.Gender);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, performance.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
