using TheatricalPlayersRefactoringKata.Application.Factories;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using System.Collections.Generic;
using System;
using TheatricalPlayersRefactoringKata.Core.UseCases;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services
{
    public class TextStatementGenerator : StatementGenerator, IStatementGenerator
    {
        private readonly PerformanceFactory _performanceFactory;

        public TextStatementGenerator(PerformanceFactory performanceFactory)
        {
            _performanceFactory = performanceFactory;
        }

        public string Generate(Invoice invoice, Dictionary<Guid, Play> plays)
        {
            return GenerateStatement(invoice, plays);
        }

        protected string GenerateStatement(Invoice invoice, Dictionary<Guid, Play> plays)
        {
            var result = $"Statement for {invoice.Customer}\n";

            foreach (var performance in invoice.Performances)
            {
                var play = plays[performance.PlayId];
                var genreString = play.Type.ToString();
                var calculator = _performanceFactory.CreateCalculator(genreString);
                decimal price = calculator.CalculatePrice(performance);
                result += $"{play.Name}: {price:C} ({performance.Audience} seats)\n";
            }

            result += $"Amount owed is {invoice.TotalAmount:C}\nYou earned {invoice.TotalCredits} credits\n";
            return result;
        }

        protected override string GenerateHeader(Invoice invoice)
        {
            return $"Statement for {invoice.Customer}\n";
        }

        protected override string GeneratePerformanceDetail(Performance performance)
        {
            var genreString = performance.Genre.ToString();
            var calculator = _performanceFactory.CreateCalculator(genreString);
            decimal price = calculator.CalculatePrice(performance);
            return $"{performance.Play.Name}: {price:C} ({performance.Audience} seats)\n";
        }

        protected override string GenerateFooter(Invoice invoice)
        {
            return $"Amount owed is {invoice.TotalAmount:C}\nYou earned {invoice.TotalCredits} credits\n";
        }
    }
}