using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Data;
using TheatricalPlayersRefactoringKata.Domain;

namespace TheatricalPlayersRefactoringKata.Application
{
    public class StatementService
    {
        public StatementData GenerateStatementData(Invoice invoice, Dictionary<string, Play> plays)
        {
            var statementData = new StatementData { Customer = invoice.Customer };
            decimal totalAmount = 0;
            int volumeCredits = 0;

            foreach (var perf in invoice.Performances)
            {
                var play = PlayFactory.CreatePlay(plays[perf.PlayId].Name, plays[perf.PlayId].Type);
                int lines = plays[perf.PlayId].Lines;

                decimal thisAmount = play.CalculateAmount(perf.Audience, lines);
                volumeCredits += play.CalculateVolumeCredits(perf.Audience);

                statementData.Items.Add(new ItemData
                {
                    PlayName = play.Name,
                    AmountOwed = Convert.ToDecimal(thisAmount) / 100,
                    EarnedCredits = play.CalculateVolumeCredits(perf.Audience),
                    Seats = perf.Audience
                });

                totalAmount += thisAmount;
            }

            statementData.TotalAmountOwed = Convert.ToDecimal(totalAmount) / 100;
            statementData.TotalEarnedCredits = volumeCredits;
            return statementData;
        }
    }
}
