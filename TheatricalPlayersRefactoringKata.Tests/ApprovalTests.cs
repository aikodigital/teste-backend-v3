using ApprovalTests;
using ApprovalTests.Reporters;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Application.Services.Gender;
using TheatricalPlayersRefactoringKata.Core.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

[UseReporter(typeof(DiffReporter))] // Usado para comparar a saída com a versão aprovada.
public class ApprovalTests
{
    private readonly StatementPrinter _statementPrinter;

    public ApprovalTests()
    {
        // Configuração inicial com os diferentes calculadores
        var calculators = new Dictionary<string, IPerformanceCalculator>
        {
            { "tragedy", new TragedyCalculator() },
            { "comedy", new ComedyCalculator() },
            { "history", new HistoryCalculator() }
        };

        _statementPrinter = new StatementPrinter(new InvoiceCalculationService(calculators), calculators);
    }

    [Fact]
    public async Task TestTextStatementExample()
    {
        // Criando instâncias de Play
        var hamlet = new Play("Hamlet", 2000, "tragedy");
        var asYouLikeIt = new Play("As You Like It", 2000, "comedy");
        var othello = new Play("Othello", 3000, "history");

        // Preparar uma fatura de teste com performances, incluindo o gênero histórico
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance("hamlet", 55, hamlet),
            new Performance("as-like", 35, asYouLikeIt),
            new Performance("othello", 40,othello)
        });

        // Gerar o extrato em texto
        var statement = await _statementPrinter.PrintAsync(invoice);

        // Aprovar o resultado gerado
        Approvals.Verify(statement);
    }

    [Fact]
    public async Task TestXmlStatementExample()
    {
        // Criando instâncias de Play
        var hamlet = new Play("Hamlet", 2000, "tragedy");
        var asYouLikeIt = new Play("As You Like It", 2000, "comedy");
        var othello = new Play("Othello", 3000, "history");

        // Preparar uma fatura de teste com performances, incluindo o gênero histórico
        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance("hamlet", 55,hamlet),
            new Performance("as-like",35, asYouLikeIt),
            new Performance("othello", 40, othello)
        });

        // Implementação necessária para gerar o extrato como XML.
        // (Certifique-se de ter um método na StatementPrinter para isso, como PrintAsXmlAsync)
        var xmlStatement = await _statementPrinter.PrintAsXmlAsync(invoice);

        // Aprovar o resultado gerado
        Approvals.VerifyXml(xmlStatement);
    }
}
