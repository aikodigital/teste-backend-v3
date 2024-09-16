using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var statement = new Statement
            {
                Customer = invoice.Customer,
                Items = new List<Item>()
            };

            decimal totalAmount = 0;
            decimal volumeCredits = 0;

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];

                decimal lines = play.Lines;
                lines = Math.Max(1000, Math.Min(lines, 4000));

                decimal thisAmount = lines * 10;
                int earnedCredits = 0;

                switch (play.Type)
                {
                    case "tragedy":
                        if (perf.Audience > 30)
                        {
                            thisAmount += 1000 * (perf.Audience - 30);
                        }
                        break;
                    case "comedy":
                        if (perf.Audience > 20)
                        {
                            thisAmount += 10000 + 500 * (perf.Audience - 20);
                        }
                        thisAmount += 300 * perf.Audience;
                        break;
                    case "history":
                        decimal tragedyAmount = thisAmount;
                        decimal comedyAmount = thisAmount + 300 * perf.Audience;

                        if (perf.Audience > 30)
                        {
                            tragedyAmount += 1000 * (perf.Audience - 30);
                        }
                        if (perf.Audience > 20)
                        {
                            comedyAmount += 10000 + 500 * (perf.Audience - 20);
                        }

                        thisAmount = tragedyAmount + comedyAmount;
                        break;
                    default:
                        throw new Exception("unknown type: " + play.Type);
                }
                // Add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);

                // Add extra credit for every ten comedy attendees
                if ("comedy" == play.Type)
                {
                    volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
                    earnedCredits = (int)Math.Floor((decimal)perf.Audience / 5);
                }
                else
                    earnedCredits = Math.Max(perf.Audience - 30, 0);

                // Add item to the statement
                statement.Items.Add(new Item
                {
                    PlayName = play.Name,
                    AmountOwed = Convert.ToDecimal(thisAmount / 100),
                    Seats = perf.Audience,
                    EarnedCredits = earnedCredits
                });

                totalAmount += thisAmount;
            }

            statement.AmountOwed = Convert.ToDecimal(totalAmount / 100);
            statement.EarnedCredits = (int)volumeCredits;

            return JsonConvert.SerializeObject(statement, Formatting.Indented);
        }
    }
}
