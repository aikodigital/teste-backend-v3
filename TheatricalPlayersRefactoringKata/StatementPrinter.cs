using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        double totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) //Para cada objeto da lista performances
        {
            var play = plays[perf.PlayId]; // Itera sobre cada performance na fatura. Para cada performance, busca o tipo de peça e calcula o custo e os créditos associados.


            var lines = play.Lines; // Calcula o valor thisAmount com base no tipo de peça e no número de linhas. Adiciona valores adicionais dependendo do tipo de peça (tragedy ou comedy).
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            double baseAmount = lines * 10;
            double thisAmount = baseAmount;
            switch (play.Type) 
            {
                case "tragedy":
                    if (perf.Audience > 30) {
                        thisAmount += 1000 * (perf.Audience - 30);
                    }
                    break;
                case "comedy":
                    if (perf.Audience > 20) {
                        thisAmount += 10000 + 500 * (perf.Audience - 20);
                    }
                    thisAmount += 300 * perf.Audience;
                    break;
                case "history":
                    var tragedyAmount = baseAmount;
                    var comedyAmount = baseAmount;

                    if (perf.Audience > 30)
                    {
                        tragedyAmount += 1000 * (perf.Audience - 30);
                    }

                    if (perf.Audience > 20)
                    {
                        comedyAmount += 10000 + 500 * (perf.Audience - 20);
                    }

                    comedyAmount += 300 * perf.Audience;
                    thisAmount = tragedyAmount + comedyAmount;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);


            }
            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;

        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}