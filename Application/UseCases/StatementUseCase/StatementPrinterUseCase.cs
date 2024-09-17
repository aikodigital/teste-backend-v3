using Domain.Contracts.UseCases.StatementUseCase;
using Domain.Entities;
using Newtonsoft.Json;

namespace Application.UseCases.StatementUseCase
{
    public class StatementPrinterUseCase : IStatementPrinterUseCase
    {
        public string Print(Invoice invoice, List<Play> plays)
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
                var play = plays.FirstOrDefault(p => p.NameId == perf.PlayId) ?? throw new Exception("Play not found for PlayId: " + perf.PlayId);

                decimal thisAmount = 0;
                int earnedCredits = 0;

                switch (play.Type)
                {
                    case "tragedy":
                        thisAmount += CalculateTragedyAmount(perf, play);
                        break;
                    case "comedy":
                        thisAmount += CalculateComedyAmount(perf, play);
                        break;
                    case "history":
                        thisAmount += CalculateHistoryAmount(perf, play);
                        break;
                    default:
                        throw new Exception("unknown type: " + play.Type);
                }

                // Add volume credits
                volumeCredits = CalculateVolumeCredits(perf, play, volumeCredits);

                // Add item to the statement
                statement.Items.Add(new Item
                {
                    PlayName = play.Name,
                    AmountOwed = Convert.ToDecimal(thisAmount / 100),
                    Seats = perf.Audience,
                    EarnedCredits = (int)CalculateEarnedCredits(perf, play, earnedCredits)
                });

                totalAmount += thisAmount;
            }

            statement.AmountOwed = Convert.ToDecimal(totalAmount / 100);
            statement.EarnedCredits = (int)volumeCredits;

            return JsonConvert.SerializeObject(statement, Newtonsoft.Json.Formatting.Indented);
        }


        private decimal CalculateBaseAmount(Play play)
        {
            decimal lines = Math.Max(1000, Math.Min(play.Lines, 4000));
            return lines * 10;
        }

        private decimal CalculateVolumeCredits(Performance perf, Play play, decimal volumeCredits)
        {
            volumeCredits += Math.Max(perf.Audience - 30, 0);

            if ("comedy" == play.Type)
                volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

            return volumeCredits;
        }

        private decimal CalculateEarnedCredits(Performance perf, Play play, decimal earnedCredits)
        {
            if ("comedy" == play.Type)
            {
                earnedCredits = Math.Max(perf.Audience - 30, 0);
                earnedCredits += (int)Math.Floor((decimal)perf.Audience / 5);
            }
            else
                earnedCredits = Math.Max(perf.Audience - 30, 0);

            return earnedCredits;
        }

        private decimal CalculateTragedyAmount(Performance perf, Play play)
        {
            decimal thisAmount = CalculateBaseAmount(play);

            if (perf.Audience > 30)
                thisAmount += 1000 * (perf.Audience - 30);

            return thisAmount;
        }

        private decimal CalculateComedyAmount(Performance perf, Play play)
        {
            decimal thisAmount = CalculateBaseAmount(play);

            if (perf.Audience > 20)
                thisAmount += 10000 + 500 * (perf.Audience - 20);

            thisAmount += 300 * perf.Audience;
            return thisAmount;
        }
        private decimal CalculateHistoryAmount(Performance perf, Play play)
        {
            decimal thisAmount = CalculateBaseAmount(play);

            decimal tragedyAmount = thisAmount;
            decimal comedyAmount = thisAmount + 300 * perf.Audience;

            if (perf.Audience > 30)
                tragedyAmount += 1000 * (perf.Audience - 30);

            if (perf.Audience > 20)
                comedyAmount += 10000 + 500 * (perf.Audience - 20);

            thisAmount = tragedyAmount + comedyAmount;

            return thisAmount;
        }
    }
}
