using TheatricalPlayersRefactoringKata.Application.Factories;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.UseCases;
using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services
{
    public class TextStatementGenerator : StatementGenerator, IStatementGenerator
    {
        protected override string GenerateHeader(Invoice invoice)
        {
            return $"Statement for {invoice.Customer}\n";
        }

        protected override string GeneratePerformanceDetail(Performance performance)
        {
            var genreString = performance.Genre.ToString();
            var calculator = PerformanceFactory.CreateCalculator(genreString);
            decimal price = calculator.CalculatePrice(performance);
            return $"{performance.Play.Name}: {price:C} ({performance.Audience} seats)\n";
        }

        protected override string GenerateFooter(Invoice invoice)
        {
            return $"Amount owed is {invoice.TotalAmount:C}\nYou earned {invoice.TotalCredits} credits\n";
        }

        public string Generate(Invoice invoice, List<Play> plays)
        {
            return GenerateStatement(invoice);
        }
    }
}