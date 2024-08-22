using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        // Simulando um banco de dados com dados em memória
        private static readonly Dictionary<int, Invoice> Invoices = new()
        {
            { 1, new Invoice("BigCo", new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                    new Performance("henry-v", 20),
                    new Performance("john", 39),
                    new Performance("richard-iii", 22)
                })
            }
        };

        private static readonly Dictionary<string, Play> Plays = new()
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") },
            { "henry-v", new Play("Henry V", 3227, "history") },
            { "john", new Play("King John", 2648, "history") },
            { "richard-iii", new Play("Richard III", 3718, "history") }
        };

        public Task<Invoice> GetInvoiceAsync(int invoiceId)
        {
            Invoices.TryGetValue(invoiceId, out var invoice);
            return Task.FromResult(invoice);
        }

        public Task<Dictionary<string, Play>> GetPlaysAsync()
        {
            return Task.FromResult(Plays);
        }
    }
}
