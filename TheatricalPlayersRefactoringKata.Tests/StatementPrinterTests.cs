using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Calculo_Credito;
using TheatricalPlayersRefactoringKata.CalculoBase;
using TheatricalPlayersRefactoringKata.Controller;
using TheatricalPlayersRefactoringKata.Factory;
using Xunit;
using Xunit.Abstractions;
using TheatricalPlayersRefactoringKata.XML;
using System.Xml;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{

    private readonly ITestOutputHelper _output;

    public StatementPrinterTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, PlayModel>
        {
            { "hamlet", new PlayModel("Hamlet", 4024, "tragedy") },
            { "as-like", new PlayModel("As You Like It", 2670, "comedy") },
            { "othello", new PlayModel("Othello", 3560, "tragedy") }
        };

        var invoice = new InvoiceModel(
            "BigCo",
            new List<PerformanceModel>
            {
                new PerformanceModel("hamlet", 55),
                new PerformanceModel("as-like", 35),
                new PerformanceModel("othello", 40),
            }
        );

        var comediaController = new CalcularComediaController();
        var tragediaController = new CalcularTragediaController();
        var historicoController = new CalcularHistoricoController(comediaController, tragediaController);


        IFazerTipos fazerTipos = new TipoPlayController(comediaController, historicoController, tragediaController);
        ICreditosEspectador calculaCreditoEspectador = new CreditoEspectador();
        StatementPrinter statementPrinter = new StatementPrinter(fazerTipos, calculaCreditoEspectador);

        var result = statementPrinter.Print(invoice, plays);
        _output.WriteLine(result);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, PlayModel>
        {
            { "hamlet", new PlayModel("Hamlet", 4024, "tragedy") },
            { "as-like", new PlayModel("As You Like It", 2670, "comedy") },
            { "othello", new PlayModel("Othello", 3560, "tragedy") },
            { "henry-v", new PlayModel("Henry V", 3227, "history") },
            { "john", new PlayModel("King John", 2648, "history") },
            { "richard-iii", new PlayModel("Richard III", 3718, "history") }
        };

        InvoiceModel invoice = new InvoiceModel(
            "BigCo",
            new List<PerformanceModel>
            {
                new PerformanceModel("hamlet", 55),
                new PerformanceModel("as-like", 35),
                new PerformanceModel("othello", 40),
                new PerformanceModel("henry-v", 20),
                new PerformanceModel("john", 39),
                new PerformanceModel("henry-v", 20)
            }
        );

        var comediaController = new CalcularComediaController();
        var tragediaController = new CalcularTragediaController();
        var historicoController = new CalcularHistoricoController(comediaController,tragediaController);


        IFazerTipos fazerTipos = new TipoPlayController(comediaController, historicoController, tragediaController);
        ICreditosEspectador calculaCreditoEspectador = new CreditoEspectador(); // Certifique-se de que o nome e a implementação estão corretos
        StatementPrinter statementPrinter = new StatementPrinter(fazerTipos, calculaCreditoEspectador);

        var result = statementPrinter.Print(invoice, plays);
        _output.WriteLine(result);

        Approvals.Verify(result);
    }


    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, PlayModel>
        {
            { "hamlet", new PlayModel("Hamlet", 4024, "tragedy") },
            { "as-like", new PlayModel("As You Like It", 2670, "comedy") },
            { "othello", new PlayModel("Othello", 3560, "tragedy") },
            { "henry-v", new PlayModel("Henry V", 3227, "history") },
            { "john", new PlayModel("King John", 2648, "history") },
            { "richard-iii", new PlayModel("Richard III", 3718, "history") }
        };

        InvoiceModel invoice = new InvoiceModel(
            "BigCo",
            new List<PerformanceModel>
            {
                new PerformanceModel("hamlet", 55),
                new PerformanceModel("as-like", 35),
                new PerformanceModel("othello", 40),
                new PerformanceModel("henry-v", 20),
                new PerformanceModel("john", 39),
                new PerformanceModel("henry-v", 20)
            }
        );

        var comediaController = new CalcularComediaController();
        var tragediaController = new CalcularTragediaController();
        var historicoController = new CalcularHistoricoController(comediaController, tragediaController);

        IFazerTipos fazerTipos = new TipoPlayController(comediaController, historicoController, tragediaController);
        ICreditosEspectador calculaCreditoEspectador = new CreditoEspectador();

        IXML serializaçaoDados = new SerializaçaoDadosXml(fazerTipos, calculaCreditoEspectador);

        var result = serializaçaoDados.SerializandoDados(invoice, plays);


        string resultString;
        using (var stringWriter = new System.IO.StringWriter())
        using (var xmlTextWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
        {
            result.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();
            resultString = stringWriter.GetStringBuilder().ToString();
        }

        Approvals.Verify(resultString);
    }

}
