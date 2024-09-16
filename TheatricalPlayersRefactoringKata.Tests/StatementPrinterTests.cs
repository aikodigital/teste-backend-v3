using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Xml;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;
using Xunit;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests   
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async Task TestStatementExampleLegacy()    
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, Genero.Tragedy));
        plays.Add("as-like", new Play("As You Like It", 2670, Genero.Comedy));
        plays.Add("othello", new Play("Othello", 3560, Genero.Tragedy));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = await statementPrinter.PrintExtrato(invoice, plays, FormatoDoExtrato.TXT);        

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async Task TestTextStatementExample()    
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, Genero.Tragedy));
        plays.Add("as-like", new Play("As You Like It", 2670, Genero.Comedy));
        plays.Add("othello", new Play("Othello", 3560, Genero.Tragedy));
        plays.Add("henry-v", new Play("Henry V", 3227, Genero.Historic));
        plays.Add("john", new Play("King John", 2648, Genero.Historic));
        plays.Add("richard-iii", new Play("Richard III", 3718, Genero.Historic));

        Invoice invoice = new Invoice(
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

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = await statementPrinter.PrintExtrato(invoice, plays, FormatoDoExtrato.TXT);        

        Approvals.Verify(result);
    }
    
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async Task TestXmlStatementExample()    
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, Genero.Tragedy));
        plays.Add("as-like", new Play("As You Like It", 2670, Genero.Comedy));
        plays.Add("othello", new Play("Othello", 3560, Genero.Tragedy));
        plays.Add("henry-v", new Play("Henry V", 3227, Genero.Historic));
        plays.Add("john", new Play("King John", 2648, Genero.Historic));
        plays.Add("richard-iii", new Play("Richard III", 3718, Genero.Historic));

        Invoice invoice = new Invoice(
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

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = await statementPrinter.PrintExtrato(invoice, plays, FormatoDoExtrato.XML);        

        // Formato de saida alterado para XML para possibilitar validação correta de cabeçalhos pertinentes
        Approvals.VerifyXml(result.ToString());        
    }
}