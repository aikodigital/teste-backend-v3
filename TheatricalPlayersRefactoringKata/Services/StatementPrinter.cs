using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Model;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{
    // Intervalo obrigatório
    private const int minimoDeLinhas = 1000;
    private const int maximoDeLinhas = 4000;

    private int AjustarLinhas(int linhas)
    {
        return Math.Clamp(linhas, minimoDeLinhas, maximoDeLinhas);
    }

    // Cálculo do valor da peça teatral, para cadastro de novos genêros basta alterar o Enum.Genero e acrescentar aqui a nova regra de negócio
    private decimal CalculaValorDaPeca(Genero genero, int audiencia, decimal valorBase = 0)
    {
        decimal calculoDaPeca = 0;

        switch (genero)
        {
            case Genero.Tragedy:
                calculoDaPeca = valorBase + ((audiencia > 30) ? 1000 * (audiencia - 30) : 0);
                break;
            case Genero.Comedy:
                calculoDaPeca = valorBase + ((audiencia > 20) ? 300 * audiencia + (10000 + 500 * (audiencia - 20)) : 300 * audiencia);
                break;
            case Genero.Historic:
                calculoDaPeca = (valorBase * 2) + ((audiencia > 30) ? 1000 * (audiencia - 30) : 0) + ((audiencia > 20) ? 300 * audiencia + (10000 + 500 * (audiencia - 20)) : 300 * audiencia);
                break;
            default:
                throw new Exception($"Gênero desconhecido: {genero}");
        }
        return calculoDaPeca;
    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    public string PrintTXT(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal somaTotal = 0;
        var creditos = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var lines = AjustarLinhas(play.Lines);
            decimal valorBase = lines * 10;
            decimal calculoDaPeca = CalculaValorDaPeca(play.Type, perf.Audience, valorBase);

            // Adiciona créditos de audiência > 30
            creditos += Math.Max(perf.Audience - 30, 0);
            // Adiciona crédito extra para cada dez pessoas em comédias
            if (play.Type == Genero.Comedy) creditos += (int)Math.Floor((decimal)perf.Audience / 5);

            // Print
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(calculoDaPeca / 100), perf.Audience);
            somaTotal += calculoDaPeca;
        }
        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(somaTotal / 100));
        result += string.Format("You earned {0} credits\n", creditos);
        return result;
    }

    public XDocument PrintXML(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal somaTotal = 0;
        var creditos = 0;
        var creditosTotais = 0;
        var cultureInfo = new CultureInfo("en-US");

        // Criando a estrutura do XML        
        var statement = new XElement("Statement",
            new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
            new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
            new XElement("Customer", invoice.Customer),
            new XElement("Items")
        );

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var lines = AjustarLinhas(play.Lines);
            decimal valorBase = lines * 10;
            decimal calculoDaPeca = CalculaValorDaPeca(play.Type, perf.Audience, valorBase);

            // Adiciona créditos de audiência > 30
            creditos = Math.Max(perf.Audience - 30, 0);
            // Adiciona crédito extra para cada dez pessoas em comédias
            if (play.Type == Genero.Comedy) creditos += (int)Math.Floor((decimal)perf.Audience / 5);
            creditosTotais += creditos;

            // Aciona itensquer
            statement.Element("Items").Add(
                new XElement("Item",
                    // Demonstra casa decimais apenas se houver e com apenas um dígito
                    new XElement("AmountOwed", (calculoDaPeca / 100) % 1 == 0 ? Math.Floor(calculoDaPeca / 100).ToString(cultureInfo) : (calculoDaPeca / 100).ToString("F1", cultureInfo)),
                    new XElement("EarnedCredits", creditos),
                    new XElement("Seats", perf.Audience)
                )
            );
            somaTotal += calculoDaPeca;
        }

        // Soma total
        statement.Add(
            new XElement("AmountOwed", (somaTotal / 100).ToString("F1", cultureInfo)),
            new XElement("EarnedCredits", creditosTotais)
        );

        var document = new XDocument(new XDeclaration("1.0", "utf-8", null), statement);

        //using (var stringWriter = new Utf8StringWriter())
        //{
        //    document.Save(stringWriter, SaveOptions.None);
        //    return stringWriter.ToString();
        //}
        return document;
    }
}
