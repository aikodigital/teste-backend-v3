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
using TheatricalPlayersRefactoringKata.Infrastructure.Data;
using TheatricalPlayersRefactoringKata.Application.Strategy;

namespace TheatricalPlayersRefactoringKata.Application;

public class StatementPrinter
{
    private readonly ICalculateBaseAmountPerLine _calculateBaseAmountPerLine;
    private readonly ICalculateCreditAudience _calculateCreditAudience;
    private readonly ICalculateAdditionalValuePerPlayType _calculateAdditionalValuePerPlayType;
    private readonly IInvoicePrintFactory _invoicePrintFactory;

    public StatementPrinter(ICalculateBaseAmountPerLine calculateBaseAmountPerLine,
                            ICalculateCreditAudience calculateCreditAudience,
                            ICalculateAdditionalValuePerPlayType calculateAdditionalValuePerPlayType,
                            IInvoicePrintFactory invoicePrintFactory)
    {
        _calculateBaseAmountPerLine = calculateBaseAmountPerLine;
        _calculateCreditAudience = calculateCreditAudience;
        _calculateAdditionalValuePerPlayType = calculateAdditionalValuePerPlayType;
        _invoicePrintFactory = invoicePrintFactory;
    }

    public string PrintInvoice(string invoiceId, string printTypeRequest)
    {
        var invoice = new Invoice();
        var performances = new List<Performance>();
        var plays = new List<Play>();
        PrintType printType;
        var invoiceCalculeteSettings = new List<InvoiceCalculeteSettings>();
        var invoiceCreditSettings = new List<InvoiceCreditSettings>();

        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            invoice = context.Invoices.Where(x => x.InvoiceId.ToString().Equals(invoiceId)).FirstOrDefault();
            if (invoice == null) throw new Exception($"invoice not found: {invoiceId}");
            performances = context.Performances.Where(x => x.InvoiceId.ToString().Equals(invoiceId)).ToList();
            if (invoice == null) throw new Exception($"invoice without performance: {invoiceId}");
            plays = context.Play.ToList();
            invoiceCalculeteSettings = context.InvoiceCalculeteSettings.ToList();
            invoiceCreditSettings = context.InvoiceCreditSettings.ToList();
        }

        switch (printTypeRequest.ToUpper())
        {
            case "XML":
                printType = PrintType.XML;
                break;
            case "TEXT":
                printType = PrintType.Text;
                break;
            default:
                throw new Exception($"unknown Print type: {printTypeRequest}");
        }

        return PrintInvoice(invoice, performances, plays, printType, invoiceCalculeteSettings, invoiceCreditSettings);
    }

    public string PrintInvoice(Invoice invoice, List<Performance> performances, List<Play> plays, PrintType printType, List<InvoiceCalculeteSettings> invoiceCalculeteSettings, List<InvoiceCreditSettings> invoiceCreditSettings)
    {
        var totalAmount = 0m;
        var volumeCredits = 0m;

        var invoicePrint = new InvoicePrint.Statement();
        invoicePrint.Customer = invoice.Customer;

        foreach (var performance in performances)
        {
            var play = plays.Where(x => x.PlayId.Equals(performance.PlayId)).FirstOrDefault();
            var thisAmount = 0m;
            var invoiceCalculetePlayType = invoiceCalculeteSettings.Where(x => x.PlayTypeId.ToString().Equals(play.PlayTypeId.ToString())).ToList();

            foreach (var invoiceCalculete in invoiceCalculetePlayType)
            {
                thisAmount += _calculateBaseAmountPerLine.CalculateBaseAmount(play.Lines);
                thisAmount += _calculateAdditionalValuePerPlayType.CalculateAdditionalValue(performance.Audience,
                                                                                          invoiceCalculete.MinimumAudience,
                                                                                          invoiceCalculete.Bonus,
                                                                                          invoiceCalculete.PerAudienceAdditional,
                                                                                          invoiceCalculete.PerAudience);
            }

            var volumeCreditPerformance = _calculateCreditAudience.CalculateCredit(performance.Audience, invoiceCreditSettings.Where(x => x.PlayTypeId.Equals(play.PlayTypeId)).FirstOrDefault());
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
