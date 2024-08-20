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

        protected override string GenerateHeader(Invoice invoice)
        {
            return $"Statement for {invoice.Customer}\n";
        }

        protected override string GeneratePerformanceDetail(Performance performance, Dictionary<Guid, Play> plays)
        {
            var play = plays[performance.PlayId];
            var calculator = _performanceFactory.CreateCalculator(play.Genre.ToString());
            decimal price = calculator.CalculatePrice(performance);
            return $"  {play.Name}: {price:C} ({performance.Audience} seats)\n";
        }

        protected override string GenerateFooter(Invoice invoice)
        {
            return $"Amount owed is {invoice.TotalAmount:C}\nYou earned {invoice.TotalCredits} credits\n";
        }
    }
}