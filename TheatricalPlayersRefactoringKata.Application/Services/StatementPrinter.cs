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
using TheatricalPlayersRefactoringKata.Domain.Exceptions;
using static TheatricalPlayersRefactoringKata.Domain.Entities.InvoicePrint;

namespace TheatricalPlayersRefactoringKata.Application;

public class StatementPrinter
{
    private readonly ICalculateBaseAmountPerLine _calculateBaseAmountPerLine;
    private readonly ICalculateCreditAudience _calculateCreditAudience;
    private readonly ICalculateAdditionalValuePerPlayType _calculateAdditionalValuePerPlayType;
    private readonly IInvoicePrintFactory _invoicePrintFactory;
    private readonly IInvoiceRepository _invoiceRepository;

    public StatementPrinter(ICalculateBaseAmountPerLine calculateBaseAmountPerLine,
                            ICalculateCreditAudience calculateCreditAudience,
                            ICalculateAdditionalValuePerPlayType calculateAdditionalValuePerPlayType,
                            IInvoicePrintFactory invoicePrintFactory,
                            IInvoiceRepository invoiceRepository)
    {
        _calculateBaseAmountPerLine = calculateBaseAmountPerLine;
        _calculateCreditAudience = calculateCreditAudience;
        _calculateAdditionalValuePerPlayType = calculateAdditionalValuePerPlayType;
        _invoicePrintFactory = invoicePrintFactory;
        _invoiceRepository = invoiceRepository;
    }

    public string PrintInvoice(string invoiceId, string printTypeRequest)
    {
        var invoice = GetInvoice(invoiceId);
        var performances = GetPerformances(invoiceId);
        var plays = GetAllPlays();
        var calcSettings = GetInvoiceCalculationSettings();
        var creditSettings = GetInvoiceCreditSettings();
        var printType = GetPrintType(printTypeRequest);

        return PrintInvoice(invoice, performances, plays, printType, calcSettings, creditSettings);
    }

    private Invoice GetInvoice(string invoiceId)
    {
        var invoice = _invoiceRepository.GetInvoiceById(invoiceId);
        if (invoice == null) throw new InvoiceNotFoundException(invoiceId);
        return invoice;
    }

    private List<Performance> GetPerformances(string invoiceId)
    {
        var performances = _invoiceRepository.GetPerformancesByInvoiceId(invoiceId);
        if (performances == null) throw new PerformanceNotFoundException(invoiceId);
        return performances;
    }

    private List<Play> GetAllPlays()
    {
        return _invoiceRepository.GetAllPlays();
    }

    private List<InvoiceCalculeteSettings> GetInvoiceCalculationSettings()
    {
        return _invoiceRepository.GetInvoiceCalculeteSettings();
    }

    private List<InvoiceCreditSettings> GetInvoiceCreditSettings()
    {
        return _invoiceRepository.GetInvoiceCreditSettings();
    }

    private PrintType GetPrintType(string printTypeRequest)
    {
        return _invoicePrintFactory.DeterminePrintType(printTypeRequest);
    }

    public string PrintInvoice(Invoice invoice, List<Performance> performances, List<Play> plays, PrintType printType,
                            List<InvoiceCalculeteSettings> invoiceCalculeteSettings, List<InvoiceCreditSettings> creditSettings)
    {
        var invoicePrint = new InvoicePrint.Statement
        {
            Customer = invoice.Customer,
            Items = new Items()
        };
        invoicePrint.Items.Item = new List<InvoicePrint.Item>();

        var totalAmount = CalculateTotalAmount(performances, plays, invoiceCalculeteSettings, creditSettings, invoicePrint);
        var totalCredits = CalculateTotalCredits(performances, plays, creditSettings);

        invoicePrint.AmountOwed = totalAmount;
        invoicePrint.EarnedCredits = totalCredits;

        var printer = _invoicePrintFactory.GetPrintType(printType);
        return printer.Print(invoicePrint);
    }

    private decimal CalculateTotalAmount(List<Performance> performances, List<Play> plays,
                                         List<InvoiceCalculeteSettings> calcSettings, List<InvoiceCreditSettings> creditSettings,
                                         InvoicePrint.Statement invoicePrint)
    {
        decimal totalAmount = 0;

        foreach (var performance in performances)
        {
            var play = FindPlayById(performance.PlayId, plays);
            if (play == null) throw new PlayNotFoundException(performance.PlayId.ToString());

            decimal playAmount = CalculatePlayAmount(play, performance, calcSettings);
            decimal earnedCredits = CalculateEarnedCredits(play, performance, creditSettings);

            totalAmount += playAmount;

            invoicePrint.Items.Item.Add(new InvoicePrint.Item
            {
                Name = play.Name,
                AmountOwed = playAmount,
                Seats = performance.Audience,
                EarnedCredits = earnedCredits
            });
        }

        return totalAmount;
    }

    private decimal CalculatePlayAmount(Play play, Performance performance, List<InvoiceCalculeteSettings> calcSettings)
    {
        var playSettings = calcSettings.Where(x => x.PlayTypeId == play.PlayTypeId).ToList();
        decimal playAmount = 0;

        foreach (var setting in playSettings)
        {
            playAmount += _calculateBaseAmountPerLine.CalculateBaseAmount(play.Lines);
            playAmount += _calculateAdditionalValuePerPlayType.CalculateAdditionalValue(
                performance.Audience,
                setting.MinimumAudience,
                setting.Bonus,
                setting.PerAudienceAdditional,
                setting.PerAudience
            );
        }

        return playAmount;
    }

    private decimal CalculateEarnedCredits(Play play, Performance performance, List<InvoiceCreditSettings> creditSettings)
    {
        var creditSetting = creditSettings.FirstOrDefault(x => x.PlayTypeId == play.PlayTypeId);
        return _calculateCreditAudience.CalculateCredit(performance.Audience, creditSetting);
    }

    private decimal CalculateTotalCredits(List<Performance> performances, List<Play> plays, List<InvoiceCreditSettings> creditSettings)
    {
        decimal totalCredits = 0;

        foreach (var performance in performances)
        {
            var play = FindPlayById(performance.PlayId, plays);
            totalCredits += CalculateEarnedCredits(play, performance, creditSettings);
        }

        return totalCredits;
    }

    private Play FindPlayById(int playId, List<Play> plays)
    {
        var play = plays.FirstOrDefault(x => x.PlayId == playId);
        if (play == null) throw new PlayNotFoundException(playId.ToString());
        return play;
    }


}
