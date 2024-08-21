using Microsoft.EntityFrameworkCore;
using Moq;
using TheatricalPlayersRefactoringKata.Infra.DataBase.Repository;
using TheatricalPlayersRefactoringKata.Infra.DataBase;
using Xunit;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;
using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Tests.RepositoryTests
{
    public class InvoiceRepositoryTest
    {
        private readonly InvoiceRepository _repository;
        private readonly Mock<IDbContextFactory<TheatricalContext>> _mockFactory;
        private readonly DbContextOptions<TheatricalContext> _options;

        public InvoiceRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<TheatricalContext>()
                .UseInMemoryDatabase(databaseName: "TheatricalDatabase")
                .Options;

            _mockFactory = new Mock<IDbContextFactory<TheatricalContext>>();
            _mockFactory.Setup(f => f.CreateDbContext()).Returns(new TheatricalContext(_options));

            _repository = new InvoiceRepository(_mockFactory.Object);
        }

        [Fact]
        public void AllInvoiceRepositoryTests()
        {
            var play1 = new Play("Odyssey", 1400, PlayType.History);
            var play2 = new Play("Hamlet", 1200, PlayType.Tragedy);
            var play3 = new Play("Macbeth", 1300, PlayType.Tragedy);

            var performance1 = new Performance { Audience = 100, Play = play1 };
            var performance2 = new Performance { Audience = 150, Play = play1 };
            var performance3 = new Performance { Audience = 200, Play = play2 };
            var performance4 = new Performance { Audience = 120, Play = play2 };
            var performance5 = new Performance { Audience = 180, Play = play3 };
            var performance6 = new Performance { Audience = 160, Play = play3 };

            var invoice1 = new Invoice("Customer1", new List<Performance> { performance1, performance2, performance3 });
            var invoice2 = new Invoice("Customer2", new List<Performance> { performance4, performance5, performance6 });

            _repository.CreateInvoice(invoice1);
            _repository.CreateInvoice(invoice2);
            var createdInvoice = _repository.GetInvoiceById(invoice1.Id);
            Assert.NotNull(createdInvoice);
            Assert.Equal(invoice1.Id, createdInvoice.Id);

            var allInvoices = _repository.GetAllInvoices();
            Assert.NotNull(allInvoices);
            Assert.Equal(2, allInvoices.Count);

            _repository.DeleteInvoice(invoice1.Id);
            var deleteInvoice = _repository.GetInvoiceById(invoice1.Id);
            Assert.Null(deleteInvoice);

        }
    }
}
