using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class StatementPrinter
    {
        private readonly StatementCalculator _statementCalculator;

        public StatementPrinter(StatementCalculator statementCalculator)
        {
            _statementCalculator = statementCalculator;
        }

        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var result = new StringBuilder();
            result.AppendLine($"Statement for {invoice.Customer}");

            decimal totalAmount = 0;
            int totalCredits = 0;

            var culture = new CultureInfo("en-US");

            foreach (var performance in invoice.Performances)
            {
                var play = plays[performance.PlayId];
                var category = _statementCalculator.GetPlayCategory(play.Category);
                decimal thisAmount = category.CalculateAmount(performance.Audience, play.Lines);
                int thisCredits = category.CalculateCredits(performance.Audience);

                result.AppendLine($"  {play.Title}: {thisAmount.ToString("C2", culture)} ({performance.Audience} seats)");

                totalAmount += thisAmount;
                totalCredits += thisCredits;
            }

            result.AppendLine($"Amount owed is {totalAmount.ToString("C2", culture)}");
            result.AppendLine($"You earned {totalCredits} credits");

            return result.ToString();
        }
    }
}
