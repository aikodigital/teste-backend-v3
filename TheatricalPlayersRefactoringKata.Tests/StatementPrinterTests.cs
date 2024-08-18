using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;
using TheatricalPlayersRefactoringKata;
using System.IO;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class StatementPrinterTests
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestXmlStatementExample()
        {
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                { "as-like", new Play("As You Like It", 2670, "comedy") },
                { "othello", new Play("Othello", 3560, "tragedy") },
                { "henry-v", new Play("Henry V", 3227, "history") },
                { "john", new Play("King John", 2648, "history") },
                { "richard-iii", new Play("Richard III", 3718, "history") }
            };

            Invoice invoice = new(
                "John Doe",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                    new Performance("henry-v", 25),
                    new Performance("john", 39),
                    new Performance("richard-iii", 20)
                }
            );

            StatementPrinter statementPrinter = new();
            var resultXml = statementPrinter.PrintXml(invoice, plays);

            using var sw = new StringWriter();
            resultXml.Save(sw);
            var xmlResult = sw.ToString();

            Approvals.VerifyXml(xmlResult);
        }

    //verifica a geração do extrato usando dados do cod legado
    [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestStatementExampleLegacy()
        {
            // Cria um dicionário de peças, associando o ID da peça com a instância da classe Play
            var plays = new Dictionary<string, Play>();
            plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
            plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
            plays.Add("othello", new Play("Othello", 3560, "tragedy"));

            // Cria uma fatura (Invoice) com uma lista de performances para um cliente específico
            Invoice invoice = new ("BigCo", new List<Performance>
                {
                    new ("hamlet", 55),
                    new ("as-like", 35),
                    new ("othello", 40),
                }
            );

            // Instancia a classe StatementPrinter e gera o extrato
            StatementPrinter statementPrinter = new();
            var result = statementPrinter.Print(invoice, plays);

            // Verifica o resultado gerado comparando-o com um arquivo aprovado
            Approvals.Verify(result);
        }

        //verifica novos extratos incluindo peças históricas
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestTextStatementExample()
        {
            // Cria um dicionário de peças, incluindo novas peças do genero "history"
            var plays = new Dictionary<string, Play>();
            plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
            plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
            plays.Add("othello", new Play("Othello", 3560, "tragedy"));
            plays.Add("henry-v", new Play("Henry V", 3227, "history"));
            plays.Add("john", new Play("King John", 2648, "history"));
            plays.Add("richard-iii", new Play("Richard III", 3718, "history"));

            // Cria uma fatura (Invoice) com uma lista de performances que inclui peças de comédia, tragédia e história
            Invoice invoice = new(
                "BigCo",
                new List<Performance>
                {
                    new ("hamlet", 55),
                    new ("as-like", 35),
                    new ("othello", 40),
                    new ("henry-v", 20),
                    new ("john", 39),
                    new ("henry-v", 20)
                }
            );

            // Instancia a classe StatementPrinter e gera o extrato com o novo conjunto de dados
            StatementPrinter statementPrinter = new ();
            var result = statementPrinter.Print(invoice, plays);

            // Verifica o resultado gerado comparando-o com um arquivo aprovado (comportamento esperado)
            Approvals.Verify(result);
        }
    }
}
