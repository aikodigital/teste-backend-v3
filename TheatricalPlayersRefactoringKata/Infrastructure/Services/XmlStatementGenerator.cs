using TheatricalPlayersRefactoringKata.Application.Factories;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.UseCases;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services
{
    public class XmlStatementGenerator : StatementGenerator
    {
        protected override string GenerateHeader(Invoice invoice)
        {
            return $"<invoice customer=\"{invoice.Customer}\">\n";
        }

        protected override string GeneratePerformanceDetail(Performance performance)
        {
            var genreString = performance.Genre.ToString();
            var calculator = PerformanceFactory.CreateCalculator(genreString);
            decimal price = calculator.CalculatePrice(performance);
            return $"\t<performance play=\"{performance.PlayId}\" seats=\"{performance.Audience}\" price=\"{price}\" />\n";
        }

        protected override string GenerateFooter(Invoice invoice)
        {
            return $"<total amount=\"{invoice.TotalAmount}\" credits=\"{invoice.TotalCredits}\" />\n</invoice>\n";
        }
    }
}