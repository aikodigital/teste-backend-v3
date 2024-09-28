using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var statementData = new List<string>();

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var thisAmount = CalculateAmount(play, perf);
            volumeCredits += CalculateVolumeCredits(play, perf);
            statementData.Add(FormatLine(play, perf, thisAmount));
            totalAmount += thisAmount;

            Console.WriteLine($"Play: {play.Name}, Lines: {play.Lines}, Audience: {perf.Audience}, ThisAmount: {thisAmount}");

        }

        return FormatStatement(invoice.Customer, totalAmount, volumeCredits, statementData);
    }

    private int CalculateAmount(Play play, Performance perf)
    {
        // Cálculo do valor base (em centavos)
        int baseAmount = Math.Clamp(play.Lines / 10, 100, 400) * 100; // Convertendo para centavos
        int thisAmount = 0;

        switch (play.Type)
        {
            case "tragedy":
                thisAmount = baseAmount; // valor base para tragédia
                if (perf.Audience > 30)
                {
                    thisAmount += 1000 * (perf.Audience - 30); // $10.00 por espectador acima de 30
                }
                break;

            case "comedy":
                thisAmount = baseAmount; // valor base para comédia
                thisAmount += 300 * perf.Audience; // $3.00 por espectador
                if (perf.Audience > 20)
                {
                    thisAmount += 10000 + 500 * (perf.Audience - 20); // $100.00 + $5.00 por espectador acima de 20
                }
                break;

            case "history":
                // A peça de história é a soma do valor de tragédia e comédia
                var tragedyAmount = CalculateAmount(new Play(play.Name, play.Lines, "tragedy"), new Performance(play.Name, perf.Audience));
                var comedyAmount = CalculateAmount(new Play(play.Name, play.Lines, "comedy"), new Performance(play.Name, perf.Audience));
                thisAmount = tragedyAmount + comedyAmount; // Soma dos valores de tragédia e comédia
                break;

            default:
                throw new Exception("Unknown type: " + play.Type);
        }

        return thisAmount;
    }
    private int CalculateVolumeCredits(Play play, Performance perf)
    {
        int credits = Math.Max(perf.Audience - 30, 0);

        if (play.Type == "comedy")
        {
            credits += (int)Math.Floor((decimal)perf.Audience / 5);
        }
        else if(play.Type == "tragedy" || play.Type == "history")
        {
            credits += 0;
        }

        return credits;
    }
    private string FormatLine(Play play, Performance perf, int amount)
    {
        CultureInfo cultureInfo = new CultureInfo("en-US");
        return String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(amount / 100), perf.Audience);
    }
    private string FormatStatement(string customer, int totalAmount, int volumeCredits, List<string> statementData)
    {
        CultureInfo cultureInfo = new CultureInfo("en-US");
        var result = new StringBuilder();
        result.AppendFormat("Statement for {0}\n", customer);

        foreach (var line in statementData)
        {
            result.Append(line);
        }

        result.AppendFormat(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result.AppendFormat("You earned {0} credits\n", volumeCredits);
        return result.ToString();
    }
}
