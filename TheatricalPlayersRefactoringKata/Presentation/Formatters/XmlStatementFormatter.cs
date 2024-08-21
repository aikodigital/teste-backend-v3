using System;
using System.Collections.Generic;
using System.Xml;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Services;

namespace TheatricalPlayersRefactoringKata.Core.Presentation.Formatters;

public class XmlStatementFormatter : IStatementFormatter
{
    private readonly StatementService _statementService;

    public XmlStatementFormatter(StatementService statementService)
    {
         _statementService = statementService;
    }

    public string Format(Invoice invoice, Dictionary<string, Play> plays)
    {
        var xmlDoc = new XmlDocument();
        var root = xmlDoc.CreateElement("Invoice");
            
        var customerElement = xmlDoc.CreateElement("Customer");
        customerElement.InnerText = invoice.Customer;
        root.AppendChild(customerElement);

        var performancesElement = xmlDoc.CreateElement("Performances");
            
        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var service = PlayTypeServiceFactory.GetService(play.Type);

            var thisAmount = service.CalculateAmount(perf, play);

            var performanceElement = xmlDoc.CreateElement("Performance");
            performanceElement.SetAttribute("Play", play.Name);
            performanceElement.SetAttribute("Amount", (thisAmount / 100).ToString());
            performanceElement.SetAttribute("Seats", perf.Audience.ToString());

            performancesElement.AppendChild(performanceElement);
        }

        root.AppendChild(performancesElement);
        xmlDoc.AppendChild(root);

        return xmlDoc.OuterXml;
    }
}

