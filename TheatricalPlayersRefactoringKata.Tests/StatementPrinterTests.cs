using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Application.Constants;
using TheatricalPlayersRefactoringKata.Application.Extensions;
using TheatricalPlayersRefactoringKata.Application.Factory;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Repository;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Application.Strategy;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Enum;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    private readonly StatementPrinter _StatementPrinter;

    public StatementPrinterTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<ICalculateBaseAmountPerLine,
                                       CalculateBaseAmountPerLine>();

        serviceCollection.AddTransient<ICalculateCreditAudience,
                                       CalculateCreditAudience>();

        serviceCollection.AddTransient<ICalculateAdditionalValuePerPlayType,
                                       CalculateAdditionalValuePerPlayType>();

        serviceCollection.AddTransient<IInvoicePrintFactory,
                                       InvoicePrintFactory>();

        serviceCollection.AddTransient<IInvoiceRepository,
                                       InvoiceRepository>();

        serviceCollection.AddTransient<StatementPrinter>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _StatementPrinter = serviceProvider.GetService<StatementPrinter>()
                                 ?? throw new InvalidOperationException("A instância de CalculateBaseAmountPerLine não pôde ser criada.");
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var invoice = new Invoice(1, "BigCo");
        var plays = new List<Play>()
        {
            new Play(1, "Hamlet", 4024, 1),
            new Play(2, "As You Like It", 2670, 2),
            new Play(3, "Othello", 3560, 1)
        };
        var performances = new List<Performance>()
        {
            new Performance(1, 1, 55, 1),
            new Performance(2, 2, 35, 1),
            new Performance(3, 3, 40, 1),
        };

        var invoiceCalculeteSettings = GenerateInvoiceCalculeteSettings();
        var invoiceCreditSettings = GenerateInvoiceCreditSettings();

        var result = _StatementPrinter.PrintInvoice(invoice, performances, plays, PrintType.Text, invoiceCalculeteSettings, invoiceCreditSettings);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = GeneratePlay();
        var invoice = GenerateInvoice();
        var performances = GeneratePerformance();
        var invoiceCreditSettings = GenerateInvoiceCreditSettings();
        var invoiceCalculeteSettings = GenerateInvoiceCalculeteSettings();
        var result = _StatementPrinter.PrintInvoice(invoice, performances, plays, PrintType.Text, invoiceCalculeteSettings, invoiceCreditSettings);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = GeneratePlay();
        var invoice = GenerateInvoice();
        var performances = GeneratePerformance();
        var invoiceCalculeteSettings = GenerateInvoiceCalculeteSettings();
        var invoiceCreditSettings = GenerateInvoiceCreditSettings();
        var result = _StatementPrinter.PrintInvoice(invoice, performances, plays, PrintType.XML, invoiceCalculeteSettings, invoiceCreditSettings);
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExampleWithDataBase()
    {
        var result = _StatementPrinter.PrintInvoice("1", "Text");

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExampleWithDataBase()
    {
        var result = _StatementPrinter.PrintInvoice("1", "XML");
        Approvals.Verify(result);
    }

    #region Generate
    private static Invoice GenerateInvoice()
    {
        return new Invoice(1, "BigCo");
    }

    private static List<Performance> GeneratePerformance()
    {
        var performance = new List<Performance>()
        {
            new Performance(1, 1, 55, 1),
            new Performance(2, 2, 35, 1),
            new Performance(3, 3, 40, 1),
            new Performance(4, 4, 20, 1),
            new Performance(5, 5, 39, 1),
            new Performance(6, 4, 20, 1)
        };
        return performance;
    }

    private static List<Play> GeneratePlay()
    {
        var plays = new List<Play>()
        {
            new Play(1, "Hamlet", 4024, 1),
            new Play(2, "As You Like It", 2670, 2),
            new Play(3, "Othello", 3560, 1),
            new Play(4, "Henry V", 3227, 3),
            new Play(5, "King John", 2648, 3),
            new Play(6, "Richard III", 3718, 3)
        };
        return plays;
    }

    private static List<InvoiceCalculeteSettings> GenerateInvoiceCalculeteSettings()
    {
        var invoiceCalculeteSettings = new List<InvoiceCalculeteSettings>();
        invoiceCalculeteSettings.Add(new InvoiceCalculeteSettings(
                    1,
                    1,
                    StatementPrinterConstants.TRAGEDY_MINIMUM_AUDIENCE,
                    StatementPrinterConstants.TRAGEDY_BONUS,
                    StatementPrinterConstants.TRAGEDY_PER_AUDIENCE_ADDITIONAL,
                    StatementPrinterConstants.TRAGEDY_PER_AUDIENCE
        ));
        invoiceCalculeteSettings.Add(new InvoiceCalculeteSettings(
                    2,
                    2,
                    StatementPrinterConstants.COMEDY_MINIMUM_AUDIENCE,
                    StatementPrinterConstants.COMEDY_BONUS,
                    StatementPrinterConstants.COMEDY_PER_AUDIENCE_ADDITIONAL,
                    StatementPrinterConstants.COMEDY_PER_AUDIENCE
        ));
        invoiceCalculeteSettings.Add(new InvoiceCalculeteSettings(
                    3,
                    3,
                    StatementPrinterConstants.TRAGEDY_MINIMUM_AUDIENCE,
                    StatementPrinterConstants.TRAGEDY_BONUS,
                    StatementPrinterConstants.TRAGEDY_PER_AUDIENCE_ADDITIONAL,
                    StatementPrinterConstants.TRAGEDY_PER_AUDIENCE
        ));
        invoiceCalculeteSettings.Add(new InvoiceCalculeteSettings(
                    4,
                    3,
                    StatementPrinterConstants.COMEDY_MINIMUM_AUDIENCE,
                    StatementPrinterConstants.COMEDY_BONUS,
                    StatementPrinterConstants.COMEDY_PER_AUDIENCE_ADDITIONAL,
                    StatementPrinterConstants.COMEDY_PER_AUDIENCE
        ));
        return invoiceCalculeteSettings;
    }
    private static List<InvoiceCreditSettings> GenerateInvoiceCreditSettings()
    {
        var InvoiceCreditSettings = new List<InvoiceCreditSettings>()
        {
            new InvoiceCreditSettings(1, 1, 30, 0),
            new InvoiceCreditSettings(2, 2, 30, 5m),
            new InvoiceCreditSettings(3, 3, 30, 0)
        };
        return InvoiceCreditSettings;
    }
    private static List<PlayType> GeneratePlayTypes()
    {
        var PlayTypes = new List<PlayType>()
        {
            new PlayType(1, "tragedy"),
            new PlayType(2, "comedy"),
            new PlayType(3, "history")
        };
        return PlayTypes;
    }
    #endregion
}
