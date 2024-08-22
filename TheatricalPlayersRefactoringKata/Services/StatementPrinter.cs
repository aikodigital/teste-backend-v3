using System.Collections.Generic;
using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Data;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class StatementPrinter
    {
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<string, IPlayCategory> _playCategories;

        public StatementPrinter(ApplicationDbContext context, Dictionary<string, IPlayCategory> playCategories)
        {
            _context = context;
            _playCategories = playCategories;
        }

        public async Task<string> PrintStatementAsync(int invoiceId, CultureInfo culture)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Performances)
                .ThenInclude(p => p.Play)
                .FirstOrDefaultAsync(i => i.Id == invoiceId);

            if (invoice == null)
                throw new ArgumentException("Invoice not found");

            var result = new StringBuilder();
            result.AppendLine($"Statement for {invoice.CustomerName}");

            decimal totalAmount = 0;
            int totalCredits = 0;

            foreach (var performance in invoice.Performances)
            {
                var play = performance.Play;
                var category = _playCategories[play.Category];
                decimal thisAmount = category.CalculateAmount(performance.Audience, play.Lines);
                int thisCredits = category.CalculateCredits(performance.Audience);

                totalAmount += thisAmount;
                totalCredits += thisCredits;

                result.AppendLine($"  {play.Title}: {thisAmount.ToString("C2", culture)} ({performance.Audience} seats)");
            }

            result.AppendLine($"Amount owed is {totalAmount.ToString("C2", culture)}");
            result.AppendLine($"You earned {totalCredits} credits");

            return result.ToString();
        }
    }
}
