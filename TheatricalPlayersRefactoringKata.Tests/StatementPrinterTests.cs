using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Data;
using TheatricalPlayersRefactoringKata.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class StatementPrinterTests
    {
        private readonly ApplicationDbContext _context;
        private readonly StatementPrinter _statementPrinter;

        public StatementPrinterTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);

            var playCategories = new Dictionary<string, IPlayCategory>
            {
                { "tragedy", new TragedyCategory() },
                { "comedy", new ComedyCategory() },
                { "history", new HistoricalCategory() }
            };

            _statementPrinter = new StatementPrinter(_context, playCategories);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var plays = new List<Play>
            {
                new Play { Title = "Hamlet", Category = "tragedy", Lines = 4024 },
                new Play { Title = "As You Like It", Category = "comedy", Lines = 2670 },
                new Play { Title = "Othello", Category = "tragedy", Lines = 3560 },
                new Play { Title = "Henry V", Category = "history", Lines = 3227 },
                new Play { Title = "King John", Category = "history", Lines = 2648 },
                new Play { Title = "Richard III", Category = "history", Lines = 3718 }
            };

            var invoice = new Invoice
            {
                CustomerName = "BigCo",
                Performances = new List<Performance>
                {
                    new Performance { Play = plays[0], Audience = 55 },
                    new Performance { Play = plays[1], Audience = 35 },
                    new Performance { Play = plays[2], Audience = 40 },
                    new Performance { Play = plays[3], Audience = 20 },
                    new Performance { Play = plays[4], Audience = 39 },
                    new Performance { Play = plays[5], Audience = 20 }
                }
            };

            _context.Plays.AddRange(plays);
            _context.Invoices.Add(invoice);
            _context.SaveChanges();
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestStatementExampleLegacy()
        {
            var invoice = await _context.Invoices
                .Include(i => i.Performances)
                .ThenInclude(p => p.Play)
                .FirstOrDefaultAsync(i => i.CustomerName == "BigCo");

            var result = await _statementPrinter.PrintStatementAsync(invoice.Id, CultureInfo.InvariantCulture);

            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestTextStatementExample()
        {
            var invoice = await _context.Invoices
                .Include(i => i.Performances)
                .ThenInclude(p => p.Play)
                .FirstOrDefaultAsync(i => i.CustomerName == "BigCo");

            var result = await _statementPrinter.PrintStatementAsync(invoice.Id, CultureInfo.InvariantCulture);

            Approvals.Verify(result);
        }
    }
}
