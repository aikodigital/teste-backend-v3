using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Factory;
using TheatricalPlayersRefactoringKata.Interface;
using TheatricalPlayersRefactoringKata.Model;

namespace TheatricalPlayersRefactoringKata.Formatter;
public class XmlStatementFormatter : IStatementFormatter
{
    public async Task<string> FormatAsync(Invoice invoice, Dictionary<string, Play> plays)
    {
        var xmlDoc = new XDocument(
            new XElement("Statement",
                new XElement("Customer", invoice.Customer),
                new XElement("Performances",
                    from perf in invoice.Performances
                    let play = plays[perf.PlayId]
                    let calculator = PlayCalculatorFactory.GetCalculator(play.Type)
                    select new XElement("Performance",
                        new XElement("Play", play.Name),
                        new XElement("Audience", perf.Audience),
                        new XElement("Amount", calculator.CalculateAmount(perf, play) / 100)
                    )
                ),
                new XElement("TotalAmount", invoice.Performances.Sum(perf =>
                    PlayCalculatorFactory.GetCalculator(plays[perf.PlayId].Type).CalculateAmount(perf, plays[perf.PlayId]) / 100)),
                new XElement("Credits", invoice.Performances.Sum(perf =>
                    PlayCalculatorFactory.GetCalculator(plays[perf.PlayId].Type).CalculateVolumeCredits(perf)))
            )
        );
        string solutionDirectory = (Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName) ?? throw new ("Não foi possível determinar o diretório da solução.");

        string outputDirectory = Path.Combine(solutionDirectory, "Xmls");
        Directory.CreateDirectory(outputDirectory);
        string filePath = Path.Combine(outputDirectory, $"{invoice.Customer}_statement.xml");
        await Task.Run(() => xmlDoc.Save(filePath));

        return xmlDoc.ToString();
    }
}

