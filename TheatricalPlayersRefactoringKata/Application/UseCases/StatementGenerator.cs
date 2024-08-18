using System.Collections.Generic;
using System;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.UseCases
{
    public abstract class StatementGenerator
    {
        protected abstract string GenerateHeader(Invoice invoice);

        protected abstract string GeneratePerformanceDetail(Performance performance, Dictionary<Guid, Play> plays);

        protected abstract string GenerateFooter(Invoice invoice);

        public string GenerateStatement(Invoice invoice, Dictionary<Guid, Play> plays)
        {
            string result = GenerateHeader(invoice);

            foreach (var performance in invoice.Performances)
            {
                result += GeneratePerformanceDetail(performance, plays);
            }

            result += GenerateFooter(invoice);
            return result;
        }
    }
}