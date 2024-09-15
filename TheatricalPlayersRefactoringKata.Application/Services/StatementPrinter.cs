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

    public string PrintInvoice(Invoice invoice, Dictionary<string, Play> plays, PrintType printType, List<InvoiceCalculeteSettings> invoiceCalculeteSettings)
    {
        var totalAmount = 0m;
        var volumeCredits = 0m;

        var invoicePrint = new InvoicePrint.Statement();
        invoicePrint.Customer = invoice.Customer;

        foreach (var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId];
            var thisAmount = 0m;
            var invoiceCalculeteGender = invoiceCalculeteSettings.Where(x => x.Gender.ToString().Equals(play.Gender.ToString())).ToList();

            foreach(var invoiceCalculete in invoiceCalculeteGender)
            {
                thisAmount += _calculateBaseAmountPerLine.CalculateBaseAmount(play.Lines);
                thisAmount += _calculateAdditionalValuePerGender.CalculateAdditionalValue(performance.Audience,
                                                                                          invoiceCalculete.MinimumAudience,
                                                                                          invoiceCalculete.Bonus,
                                                                                          invoiceCalculete.PerAudienceAdditional,
                                                                                          invoiceCalculete.PerAudience);
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
