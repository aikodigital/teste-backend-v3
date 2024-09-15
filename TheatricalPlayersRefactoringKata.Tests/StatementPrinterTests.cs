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
        // Configurar o container de serviços
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<ICalculateBaseAmountPerLine,
                                       CalculateBaseAmountPerLine>();

        serviceCollection.AddTransient<ICalculateCreditAudience,
                                       CalculateCreditAudience>();

        serviceCollection.AddTransient<ICalculateAdditionalValuePerGender,
                                       CalculateAdditionalValuePerGender>();

        serviceCollection.AddTransient<IInvoicePrintFactory,
                                       InvoicePrintFactory>();

        serviceCollection.AddTransient<StatementPrinter>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _StatementPrinter = serviceProvider.GetService<StatementPrinter>()
                                 ?? throw new InvalidOperationException("A instância de CalculateBaseAmountPerLine não pôde ser criada.");
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        var invoiceCalculeteSettings = GenerateInvoiceCalculeteSettings();

        var result = _StatementPrinter.PrintInvoice(invoice, plays, PrintType.Text, invoiceCalculeteSettings);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = GeneratePlay();
        var invoice = GenerateInvoice();
        var invoiceCalculeteSettings = GenerateInvoiceCalculeteSettings();
        var result = _StatementPrinter.PrintInvoice(invoice, plays, PrintType.Text, invoiceCalculeteSettings);

        Approvals.Verify(result);
    }
        
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = GeneratePlay();
        var invoice = GenerateInvoice();
        var invoiceCalculeteSettings = GenerateInvoiceCalculeteSettings();
        var result = _StatementPrinter.PrintInvoice(invoice, plays, PrintType.XML, invoiceCalculeteSettings);
        Approvals.Verify(result);
    }

    #region Generate
    private static Invoice GenerateInvoice()
    {
        return new Invoice(
                    "BigCo",
                    new List<Performance>
                    {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                new Performance("henry-v", 20),
                new Performance("john", 39),
                new Performance("henry-v", 20)
                    }
                );
    }

    private static Dictionary<string, Play> GeneratePlay()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));
        plays.Add("henry-v", new Play("Henry V", 3227, "history"));
        plays.Add("john", new Play("King John", 2648, "history"));
        plays.Add("richard-iii", new Play("Richard III", 3718, "history"));
        return plays;
    }

    private static List<InvoiceCalculeteSettings> GenerateInvoiceCalculeteSettings()
    {
        var invoiceCalculeteSettings = new List<InvoiceCalculeteSettings>();
        invoiceCalculeteSettings.Add(new InvoiceCalculeteSettings(
                    new Guid(),
                    GenderType.tragedy,
                    StatementPrinterConstants.TRAGEDY_MINIMUM_AUDIENCE,
                    StatementPrinterConstants.TRAGEDY_BONUS,
                    StatementPrinterConstants.TRAGEDY_PER_AUDIENCE_ADDITIONAL,
                    StatementPrinterConstants.TRAGEDY_PER_AUDIENCE
        ));
        invoiceCalculeteSettings.Add(new InvoiceCalculeteSettings(
                    new Guid(),
                    GenderType.comedy,
                    StatementPrinterConstants.COMEDY_MINIMUM_AUDIENCE,
                    StatementPrinterConstants.COMEDY_BONUS,
                    StatementPrinterConstants.COMEDY_PER_AUDIENCE_ADDITIONAL,
                    StatementPrinterConstants.COMEDY_PER_AUDIENCE
        ));
        invoiceCalculeteSettings.Add(new InvoiceCalculeteSettings(
                    new Guid(),
                    GenderType.history,
                    StatementPrinterConstants.TRAGEDY_MINIMUM_AUDIENCE,
                    StatementPrinterConstants.TRAGEDY_BONUS,
                    StatementPrinterConstants.TRAGEDY_PER_AUDIENCE_ADDITIONAL,
                    StatementPrinterConstants.TRAGEDY_PER_AUDIENCE
        ));
        invoiceCalculeteSettings.Add(new InvoiceCalculeteSettings(
                    new Guid(),
                    GenderType.history,
                    StatementPrinterConstants.COMEDY_MINIMUM_AUDIENCE,
                    StatementPrinterConstants.COMEDY_BONUS,
                    StatementPrinterConstants.COMEDY_PER_AUDIENCE_ADDITIONAL,
                    StatementPrinterConstants.COMEDY_PER_AUDIENCE
        ));
        return invoiceCalculeteSettings;
    }
    #endregion
}
