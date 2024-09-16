using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Linq;

using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.ViewModels;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter : CalculadoraDePecas
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    // Controle dos formatos de saída
    public string PrintExtrato(Invoice invoice, Dictionary<string, Play> plays, FormatoDoExtrato formatoDoEstrato)
    {
        var (somaTotal, creditosTotais, detalhesDaApresentacao) = GerarDetalhesDePerformance(invoice, plays);

        switch (formatoDoEstrato)
        {
            case FormatoDoExtrato.TXT:
                return FormatarSaidaTXT(invoice.Customer, detalhesDaApresentacao, somaTotal, creditosTotais);

            case FormatoDoExtrato.XML:
                return FormatarSaidaXML(invoice.Customer, detalhesDaApresentacao, somaTotal, creditosTotais);

            default:
                throw new ArgumentException("Formato do extrato não suportado.", nameof(formatoDoEstrato));
        }
    }

    // Cáculo dos dados comuns a todos tipos de saída - Via Tupla
    private (decimal somaTotal, int creditosTotais, List<DetalhesDaApresentacaoViewModel> detalhesDaApresentacao) GerarDetalhesDePerformance(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal somaTotal = 0;
        int creditosTotais = 0;
        var detalhesDaApresentacao = new List<DetalhesDaApresentacaoViewModel>();

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var lines = AjustarQuantidadeDeLinhas(play.Lines);
            decimal valorBase = lines * 10;
            decimal calculoDaPeca = CalculaValorDaPeca(play.Type, perf.Audience, valorBase);

            // Adiciona créditos de audiência > 30
            int creditos = Math.Max(perf.Audience - 30, 0);
            // Adiciona crédito extra para cada dez pessoas em comédias
            if (play.Type == Genero.Comedy) creditos += (int)Math.Floor((decimal)perf.Audience / 5);

            creditosTotais += creditos;

            detalhesDaApresentacao.Add(new DetalhesDaApresentacaoViewModel
            {
                PlayName = play.Name,
                AmountOwed = calculoDaPeca,
                EarnedCredits = creditos,
                Seats = perf.Audience
            });

            somaTotal += calculoDaPeca;
        }

        return (somaTotal, creditosTotais, detalhesDaApresentacao);
    }

    // Formata saída em TXT conforme estrutura necessária
    private string FormatarSaidaTXT(string customer, List<DetalhesDaApresentacaoViewModel> detalhesDaApresentacao, decimal somaTotal, int creditosTotais)
    {
        var result = string.Format("Statement for {0}\n", customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var detail in detalhesDaApresentacao)
        {
            // Adiciona itens TXT
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", detail.PlayName, detail.AmountOwed, detail.Seats);
        }

        // Adiciona totalizadores ao final do arquivo TXT
        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", somaTotal);
        result += string.Format("You earned {0} credits\n", creditosTotais);

        return result;
    }

    // Formata saída em XML conforme estrutura necessária
    private string FormatarSaidaXML(string customer, List<DetalhesDaApresentacaoViewModel> detalhesDaApresentacao, decimal somaTotal, int creditosTotais)
    {
        CultureInfo cultureInfo = new CultureInfo("en-US");

        // Criando a estrutura do XML 
        var statement = new XElement("Statement",
            new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
            new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
            new XElement("Customer", customer),
            new XElement("Items")
        );

        // Adiciona itens XML
        foreach (var detalhe in detalhesDaApresentacao)
        {
            statement.Element("Items").Add(
                new XElement("Item",
                    new XElement("AmountOwed", (detalhe.AmountOwed) % 1 == 0 ? Math.Floor(detalhe.AmountOwed).ToString(cultureInfo) : (detalhe.AmountOwed).ToString("F1", cultureInfo)),
                    new XElement("EarnedCredits", detalhe.EarnedCredits),
                    new XElement("Seats", detalhe.Seats)
                )
            );
        }

        // Adiciona totalizadores ao final do arquivo XML
        statement.Add(
            new XElement("AmountOwed", somaTotal.ToString("F1", cultureInfo)),
            new XElement("EarnedCredits", creditosTotais)
        );

        var document = new XDocument(new XDeclaration("1.0", "utf-8", null), statement);

        using (var stringWriter = new Utf8StringWriter())
        {
            document.Save(stringWriter, SaveOptions.None);
            return stringWriter.ToString();
        }
    }
}