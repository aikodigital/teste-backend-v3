using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TheatricalPlayersRefactoringKata.Contracts;

namespace TheatricalPlayersRefactoringKata;

public class Statement
{
    readonly Dictionary<IPerformance, StatementEntry> registry = new (32);

    public Invoice Invoice { get; init; }

    public string Customer => Invoice.Customer;

    public int Count => registry.Count;
    public decimal TotalAmount { get; private set; }
    public int TotalCredits { get; private set; }

    public void Add(IPerformance performance, StatementEntry entry)
    {
        registry.Add(performance, entry);
    }
    public StatementEntry? Get(IPerformance performance)
    {
        if (registry.TryGetValue(performance, out StatementEntry entry))
            return entry;
        return null;
    }

    public decimal CalculateTotal()
    {
        decimal total = 0;
        foreach (var performance in registry)
        {
            total += performance.Value.Amount;
        }

        TotalAmount = total;
        
        return total;
    }

    public decimal TotalFor(string playName)
    {
        decimal total = 0;
        foreach (var performance in registry)
        {
            if (performance.Key.Play.Name == playName)
            {
                total += performance.Value.Amount;
            }
        }
        
        return total;
    }

    public decimal TotalFor<TPerformance>() where TPerformance : IPerformance
    {
        decimal total = 0;

        foreach(var performance in registry)
        {
            if (performance.Key is TPerformance)
            {
                total += performance.Value.Amount;
            }
        }

        return total;
    }

    public Dictionary<IPerformance, StatementEntry>.Enumerator GetEnumerator() => registry.GetEnumerator();

    public Statement(Invoice invoice)
    {
        Invoice = invoice;
        Build(invoice);
    }
    void Build(Invoice invoice)
    {
        decimal totalAmount = 0m;
        var volumeCredits = 0;

        foreach (var perf in invoice.Performances)
        {
            var play = perf.Play;
            decimal lines = play.Lines;

            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            decimal baseAmount = lines / 10;

            decimal thisAmount = baseAmount;

            int thisCredits = 0;

            thisAmount += perf.CalculateAmmount();

            thisCredits += perf.CalculateCredits();

            // add credits
            thisCredits += Math.Max(perf.Audience - 30, 0);

            volumeCredits += thisCredits;

            // print line for this order
            totalAmount += thisAmount;

            Add(perf, new StatementEntry(thisAmount, thisCredits));
        }
        TotalAmount = totalAmount;
        TotalCredits = volumeCredits;
    }
}
