using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Application.Constants;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Services;
using System.Reflection;
using TheatricalPlayersRefactoringKata.Domain.Enum;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata.Application;

public class StatementPrinter
{
    private readonly ICalculateBaseAmountPerLine _calculateBaseAmountPerLine;
    private readonly ICalculateCreditAudience _calculateCreditAudience;
    private readonly ICalculateAdditionalValuePerGender _calculateAdditionalValuePerGender;
    private readonly IInvoicePrintFactory _invoicePrintFactory;

    public StatementPrinter(ICalculateBaseAmountPerLine calculateBaseAmountPerLine,
                            ICalculateCreditAudience calculateCreditAudience,
                            ICalculateAdditionalValuePerGender calculateAdditionalValuePerGender,
                            IInvoicePrintFactory invoicePrintFactory)
    {
        _calculateBaseAmountPerLine = calculateBaseAmountPerLine;
        _calculateCreditAudience = calculateCreditAudience;
        _calculateAdditionalValuePerGender = calculateAdditionalValuePerGender;
        _invoicePrintFactory = invoicePrintFactory;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays, PrintType printType)
    {
        var totalAmount = 0m;
        var volumeCredits = 0m;

        var invoicePrint = new InvoicePrint.Statement();
        invoicePrint.Customer = invoice.Customer;

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

            var volumeCreditPerformance = _calculateCreditAudience.CalculateCredit(performance.Audience, play.Gender);
            volumeCredits += volumeCreditPerformance;

            // print line for this order
            invoicePrint.Items.Item.Add(new InvoicePrint.Item()
            {
                Name = play.Name,
                AmountOwed = thisAmount,
                EarnedCredits = volumeCreditPerformance,
                Seats = performance.Audience
            });

            totalAmount += thisAmount;
        }
        invoicePrint.AmountOwed = totalAmount;
        invoicePrint.EarnedCredits = volumeCredits;


        var _invoicePrint = _invoicePrintFactory.GetPrintType(printType);
        return _invoicePrint.Print(invoicePrint);
    }
}
