using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.UseCases
{
    public abstract class StatementGenerator
    {
        protected abstract string GenerateHeader(Invoice invoice);
        protected abstract string GeneratePerformanceDetail(Performance performance);
        protected abstract string GenerateFooter(Invoice invoice);

        public string GenerateStatement(Invoice invoice)
        {
            string result = GenerateHeader(invoice);

            foreach (var performance in invoice.Performances)
            {
                result += GeneratePerformanceDetail(performance);
            }

            result += GenerateFooter(invoice);
            return result;
        }
    }
}