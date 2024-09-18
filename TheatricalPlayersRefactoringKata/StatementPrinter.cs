using Domain.DTOs;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    

  

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var thisAmount = Calculates.CalculatePlayAmount(perf, play);
            volumeCredits += Calculates.CalculateVolumeCredits(perf.Audience, play);

            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
    public IList<ReportDTO> Report(InvoiceDTO invoice, IList<TheaterPlayDTO> theaterplays)
    {
        decimal totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");
        var repots = new List<ReportDTO>();
        foreach (var performance in invoice.Performances)
        {  
            var play = theaterplays.Where(p => p.PlayId == performance.PlayId).Select(p => new Play(p.Play.Name, p.Play.Lines, p.Play.Type)).FirstOrDefault();
            var perf = new Performance( play.Name, performance.Audience);


            var thisAmount = Calculates.CalculatePlayAmount(perf, play);
            volumeCredits += Calculates.CalculateVolumeCredits(perf.Audience, play);

            //result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;

            var report = new ReportDTO
            {
                PlayId = performance.PlayId,
                Play = theaterplays.FirstOrDefault(p => p.PlayId == performance.PlayId).Play,
                InvoiceId = invoice.Id,
                Invoice = invoice,
                Amount = Convert.ToDecimal(thisAmount / 100),
                Seats = perf.Audience,
                Name = play.Name,
            };
            repots.Add(report);
        }
        return repots;
    }
    public ReportCreditEntity ReportCredits(IList<ReportDTO> reports)
    {
        var result = new ReportCreditEntity();
        foreach (var item in reports)
        { var performance = item.Invoice.Performances.FirstOrDefault(p => p.PlayId == item.PlayId);
            result.AmountTotal += item.Amount;
            result.Credits += Calculates.CalculateVolumeCredits(performance.Audience, new Play (  item.Play.Name, item.Play.Lines, item.Play.Type ));
        }
        return result;
    }
    public string Xml(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        var volumeCredits = 0;
        CultureInfo cultureInfo = new CultureInfo("en-US");

        var statementElement = new XElement("Statement",
          new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
          new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
          new XElement("Customer", invoice.Customer),
          new XElement("Items")
        );

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var thisAmount = Calculates.CalculatePlayAmount(perf, play);
            var credits = Calculates.CalculateVolumeCredits(perf.Audience, play);

            var itemElement = new XElement("Item",
                new XElement("AmountOwed", Convert.ToDecimal(thisAmount / 100).ToString("0.##", cultureInfo)),
                new XElement("EarnedCredits", credits),
                new XElement("Seats", perf.Audience)
            );
            volumeCredits += credits;
            statementElement.Element("Items").Add(itemElement);
            totalAmount += thisAmount;
        }

        statementElement.Add(new XElement("AmountOwed", Convert.ToDecimal(totalAmount / 100).ToString("0.##", cultureInfo)));
        statementElement.Add(new XElement("EarnedCredits", volumeCredits));

        var document = new XDocument(new XDeclaration("1.0", "utf-8", null), statementElement);
        return "﻿<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" + document.ToString();
    }





}
