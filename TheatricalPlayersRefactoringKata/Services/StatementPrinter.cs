using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using System.IO;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Models;
using TheatricalPlayersRefactoringKata.Domain.DTO;

public class StatementPrinter
{
    private readonly ITypeCalculatorFactory _calculatorFactory;

    public StatementPrinter(ITypeCalculatorFactory calculatorFactory)
    {
        _calculatorFactory = calculatorFactory;
    }

    public StatementDTO GenerateStatement(Invoice invoice, Dictionary<string, Play> plays)
    {
        double totalAmount = 0;
        double volumeCredits = 0;
        var items = new List<ItemDTO>();

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayName];
            perf.Play = play;

            var calculator = _calculatorFactory.GetCalculator(play.Type);
            var thisAmount = calculator.Calculate(perf);
            var thisVolumeCredits = calculator.CalculateCredits(perf);

            // Add volume credits
            volumeCredits += thisVolumeCredits;

            // Add item to the list
            items.Add(new ItemDTO
            {
                ItemName = play.Name,
                AmountOwed = Math.Round(thisAmount / 100, 2),
                EarnedCredits = thisVolumeCredits,
                Seats = perf.Audience
            });

            totalAmount += thisAmount;
        }

        return new StatementDTO
        {
            Customer = invoice.Customer,
            Items = items,
            AmountOwed = Math.Round(totalAmount / 100, 2),
            EarnedCredits = volumeCredits
        };
    }
}