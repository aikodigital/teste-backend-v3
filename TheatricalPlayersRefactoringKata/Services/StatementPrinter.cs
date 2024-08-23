using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Models;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Numerics;
using TheatricalPlayersRefactoringKata.Data;
using Microsoft.EntityFrameworkCore;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{
    private readonly PlayType[] PlayTypes;
    private readonly Dictionary<string, Play> Plays;
    private readonly AppDbContext db = null;
    public StatementPrinter(Dictionary<string, Play> plays, PlayType[] playTypes)
    {
        Plays = plays;
        PlayTypes = playTypes;
    }

    public StatementPrinter(AppDbContext db)
    {
        this.db = db;
        Plays = db.Plays.ToDictionary(play => play.Name);
        PlayTypes = db.PlayTypes.ToArray();
    }

    private IPlayCalculator GetCalculatorByType(string playType)
    {
        if (!PlayTypes.Any(p => p.Name.ToLower() == playType.ToLower()))
            throw new ArgumentOutOfRangeException($"unknown type: {playType}");

        switch (playType.ToLower())
        {
            case "tragedy": return new TragedyCalculator();
            case "comedy": return new ComedyCalculator();
            case "history": return new HistoryCalculator();
            default: throw new NotImplementedException($"calculator not implemented for type: {playType}");
        }
    }

    public string PrintText(Invoice invoice)
    {
        var billingStatement = new StringBuilder($"Statement for {invoice.Customer}\n");
        try
        {
            decimal totalAmount = 0m;
            int volumeCredits = 0;
            CultureInfo cultureInfo = new CultureInfo("en-US");
            DateTime dt = DateTime.Now;

            foreach (Performance perf in invoice.Performances)
            {
                var play = Plays.FirstOrDefault(p => p.Key.ToLower() == perf.PlayId.ToLower()).Value;

                if (play is null) throw new ArgumentOutOfRangeException($"{perf.PlayId} is not a valid Play.");

                var playTypeCalculator = GetCalculatorByType(play.Type);
                decimal thisAmount = playTypeCalculator.CalculateAmount(play, perf.Audience);
                volumeCredits += playTypeCalculator.CalculateCredits(play, perf.Audience);

                billingStatement.AppendLine(cultureInfo, $"  {play.Name}: {thisAmount:C} ({perf.Audience} seats)");
                if (db != null)
                {
                    db.StatementLogs.Add(new StatementLog
                    {
                        DtInclusao = dt,
                        PlayId = perf.PlayId,
                        Costumer = invoice.Customer,
                        Audience = perf.Audience,
                        Amount = thisAmount,
                        Credits = volumeCredits
                    });
                    db.SaveChanges();
                }

                totalAmount += thisAmount;
            }
            billingStatement.AppendLine(cultureInfo, $"Amount owed is {totalAmount:C}");
            billingStatement.AppendLine($"You earned {volumeCredits} credits");
        }
        catch { throw; }
        return billingStatement.ToString();
    }

    public string PrintXml(Invoice invoice)
    {
        try
        {
            decimal totalAmount = 0m;
            int volumeCredits = 0;
            DateTime dt = DateTime.Now;

            XElement[] items = invoice.Performances.Select(perf =>
            {
                Play? play = Plays.FirstOrDefault(p => p.Key.ToLower() == perf.PlayId.ToLower()).Value;

                if (play is null) throw new ArgumentOutOfRangeException($"{perf.PlayId} is not a valid Play.");

                var playTypeCalculator = GetCalculatorByType(play.Type);
                decimal thisAmount = playTypeCalculator.CalculateAmount(play, perf.Audience);
                int thisCredits = playTypeCalculator.CalculateCredits(play, perf.Audience);

                totalAmount += thisAmount;
                volumeCredits += thisCredits;

                if (db != null)
                {
                    db.StatementLogs.Add(new StatementLog
                    {
                        DtInclusao = dt,
                        PlayId = perf.PlayId,
                        Costumer = invoice.Customer,
                        Audience = perf.Audience,
                        Amount = thisAmount,
                        Credits = volumeCredits
                    });
                    db.SaveChanges();
                }

                return new XElement("Item",
                           new XElement("AmountOwed", thisAmount),
                           new XElement("EarnedCredits", thisCredits),
                           new XElement("Seats", perf.Audience));

            }).ToArray();

            var xmlDoc = new XDocument(
                             new XDeclaration("1.0", "UTF-8", null),
                             new XElement("Statement",
                                 new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                                 new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                                 new XElement("Customer", invoice.Customer),
                                 new XElement("Items", items),
                                 new XElement("AmountOwed", totalAmount),
                                 new XElement("EarnedCredits", volumeCredits)
                                        )
                            );

            string xmlString = string.Empty;
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                xmlDoc.Save(writer);
                xmlString = Encoding.UTF8.GetString(stream.ToArray());
            }
            return xmlString;
        }
        catch { throw; }
    }
}
