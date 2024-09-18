using aikodigital_teste_backend_v3;
using aikodigital_teste_backend_v3.Data;
using aikodigital_teste_backend_v3.Models;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace aikodigital_teste_backend_v3_tests
{
    public class StatementPrinterTests
    {
        private Mock<ApplicationDbContext> CreateMockDbContext()
        {
            Mock<ApplicationDbContext> mockContext = new Mock<ApplicationDbContext>();
            return mockContext;
        }

        private ApplicationDbContext CreateInMemoryContext()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Ensures each test uses a unique instance
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        [Obsolete]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestStatementExampleLegacy()
        {
            Dictionary<string, Play> plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play { Name = "Hamlet", Lines = 4024, Type = "tragedy" } },
                { "as-like", new Play { Name = "As You Like It", Lines = 2670, Type = "comedy" } },
                { "othello", new Play { Name = "Othello", Lines = 3560, Type = "tragedy" } }
            };

            List<Performance> performances = new List<Performance>
            {
                new Performance { PlayId = "hamlet", Audience = 55 },
                new Performance { PlayId = "as-like", Audience = 35 },
                new Performance { PlayId = "othello", Audience = 40 }
            };

            Invoice invoice = new Invoice
            {
                Customer = "BigCo",
                Performances = performances
            };

            Mock<ApplicationDbContext> mockContext = CreateMockDbContext();
            StatementPrinterService statementPrinterService = new StatementPrinterService(mockContext.Object);
            string result = await statementPrinterService.GenerateTxtData(invoice, plays);

            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestTextStatementExample()
        {
            Dictionary<string, Play> plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play { Name = "Hamlet", Lines = 4024, Type = "tragedy" } },
                { "as-like", new Play { Name = "As You Like It", Lines = 2670, Type = "comedy" } },
                { "othello", new Play { Name = "Othello", Lines = 3560, Type = "tragedy" } },
                { "henry-v", new Play { Name = "Henry V", Lines = 3227, Type = "history" } },
                { "john", new Play { Name = "King John", Lines = 2648, Type = "history" } },
                { "richard-iii", new Play { Name = "Richard III", Lines = 3718, Type = "history" } }
            };

            List<Performance> performances = new List<Performance>
            {
                new Performance { PlayId = "hamlet", Audience = 55 },
                new Performance { PlayId = "as-like", Audience = 35 },
                new Performance { PlayId = "othello", Audience = 40 },
                new Performance { PlayId = "henry-v", Audience = 20 },
                new Performance { PlayId = "john", Audience = 39 },
                new Performance { PlayId = "henry-v", Audience = 20 }
            };

            Invoice invoice = new Invoice
            {
                Customer = "BigCo",
                Performances = performances
            };

            Mock<ApplicationDbContext> mockContext = CreateMockDbContext();
            StatementPrinterService statementPrinterService = new StatementPrinterService(mockContext.Object);
            string result = await statementPrinterService.GenerateTxtData(invoice, plays);

            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestXmlStatementExample()
        {
            Dictionary<string, Play> plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play { Name = "Hamlet", Lines = 4024, Type = "tragedy" } },
                { "as-like", new Play { Name = "As You Like It", Lines = 2670, Type = "comedy" } },
                { "othello", new Play { Name = "Othello", Lines = 3560, Type = "tragedy" } },
                { "henry-v", new Play { Name = "Henry V", Lines = 3227, Type = "history" } },
                { "john", new Play { Name = "King John", Lines = 2648, Type = "history" } },
                { "richard-iii", new Play { Name = "Richard III", Lines = 3718, Type = "history" } }
            };

            List<Performance> performances = new List<Performance>
            {
                new Performance { PlayId = "hamlet", Audience = 55 },
                new Performance { PlayId = "as-like", Audience = 35 },
                new Performance { PlayId = "othello", Audience = 40 },
                new Performance { PlayId = "henry-v", Audience = 20 },
                new Performance { PlayId = "john", Audience = 39 },
                new Performance { PlayId = "henry-v", Audience = 20 }
            };

            Invoice invoice = new Invoice
            {
                Customer = "BigCo",
                Performances = performances
            };

            Mock<ApplicationDbContext> mockContext = CreateMockDbContext();
            StatementPrinterService statementPrinterService = new StatementPrinterService(mockContext.Object);

            string xmlResult = await statementPrinterService.GenerateXmlData(invoice, plays);

            Approvals.Verify(xmlResult);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestSaveStatementInDb()
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            using (ApplicationDbContext context = new ApplicationDbContext(options))
            {
                // Create a set of performances
                List<Performance> performances = new List<Performance>
                {
                    new Performance { PlayId = "hamlet", Audience = 35 },
                    new Performance { PlayId = "hamlet", Audience = 25 },
                    new Performance { PlayId = "hamlet", Audience = 15 }
                };

                Invoice invoice = new Invoice
                {
                    Customer = "BigCo",
                    Performances = performances
                };

                string statement = "Dummy Statement";

                StatementPrinterService statementPrinterService = new StatementPrinterService(context);

                // Save the statement in the database
                await statementPrinterService.SaveStatementInDb(invoice, statement);

                // Check if the PerformanceStatement was saved
                List<PerformanceStatement> savedStatements = context.PerformanceStatement.ToList();

                Assert.NotEmpty(savedStatements); // Check if there is any saved data
                Assert.Equal("BigCo", savedStatements[0].Customer);
                Assert.Equal("hamlet", savedStatements[0].PlayId);
                Assert.Equal(35, savedStatements[0].Audience);

                Assert.Equal("BigCo", savedStatements[1].Customer);
                Assert.Equal("hamlet", savedStatements[1].PlayId);
                Assert.Equal(25, savedStatements[1].Audience);

                Assert.Equal("BigCo", savedStatements[2].Customer);
                Assert.Equal("hamlet", savedStatements[2].PlayId);
                Assert.Equal(15, savedStatements[2].Audience);
            }
        }
    }
}