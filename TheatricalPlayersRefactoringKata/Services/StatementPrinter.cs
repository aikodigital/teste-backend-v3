using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services.Base;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter : MainService
{
    public CultureInfo cultureInfo { get; set; } = new CultureInfo("en-US");
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = $"Statement for {invoice.Customer}\n";

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var thisAmount = CalculateAmount(play, perf);
            volumeCredits += CalculateVolumeCredits(play, perf);

            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount / 100m, perf.Audience);
            totalAmount += thisAmount;
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100m);
        result += $"You earned {volumeCredits} credits\n";
        return result;
    }

    public XDocument PrintAsXml(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;

        // Cria o elemento raiz do XML
        var statementElement = new XElement("Statement",
            new XAttribute("Customer", invoice.Customer)
        );

        // Itera sobre as performances
        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var thisAmount = CalculateAmount(play, perf);
            volumeCredits += CalculateVolumeCredits(play, perf);

            // Adiciona cada performance como um elemento no XML
            var performanceElement = new XElement("Performance",
                new XAttribute("Play", play.Name),
                new XAttribute("Amount", (thisAmount / 100m).ToString("C", cultureInfo)),
                new XAttribute("Seats", perf.Audience)
            );

            statementElement.Add(performanceElement);
            totalAmount += thisAmount;
        }

        // Adiciona os totais no XML
        statementElement.Add(new XElement("TotalAmount",
            (totalAmount / 100m).ToString("C", cultureInfo))
        );

        statementElement.Add(new XElement("Credits",
            volumeCredits)
        );

        // Retorna o documento XML
        return new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            statementElement
        );
    }
}
