using ApprovalTests;
using ApprovalTests.Reporters;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Service.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    private readonly Mock<ITypeGenreRepository> _mockTypeGenreRepository;
    private readonly Mock<ICustomerStatementRepository> _iCustomerStatementRepository;

    public StatementPrinterTests()
    {
        _mockTypeGenreRepository = new Mock<ITypeGenreRepository>();
        _iCustomerStatementRepository = new Mock<ICustomerStatementRepository>();
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    //[Fact]
    //[UseReporter(typeof(DiffReporter))]
    //public void TestTextStatementExample()
    //{
    //    var plays = new Dictionary<string, Play>();
    //    plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
    //    plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
    //    plays.Add("othello", new Play("Othello", 3560, "tragedy"));
    //    plays.Add("henry-v", new Play("Henry V", 3227, "history"));
    //    plays.Add("john", new Play("King John", 2648, "history"));
    //    plays.Add("richard-iii", new Play("Richard III", 3718, "history"));

    //    Invoice invoice = new Invoice(
    //        "BigCo",
    //        new List<Performance>
    //        {
    //            new Performance("hamlet", 55),
    //            new Performance("as-like", 35),
    //            new Performance("othello", 40),
    //            new Performance("henry-v", 20),
    //            new Performance("john", 39),
    //            new Performance("henry-v", 20)
    //        }
    //    );

    //    StatementPrinter statementPrinter = new StatementPrinter();
    //    var result = statementPrinter.Print(invoice, plays);

    //    Approvals.Verify(result);
    //}

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async Task TestTextStatementExample()
    {
        var typeGenres = new Dictionary<string, TypeGenre>
        {
            { "tragedy", new TypeGenre { Name = "tragedy", BasePriceMultiplier = 10, MaxAudience = 30, ExtraFeePerAudience = 1000 } },
            { "comedy", new TypeGenre { Name = "comedy", BasePriceMultiplier = 10, MaxAudience = 20, ExtraFeePerAudience = 500, BaseFeePerAudience = 300, BonusFee = 10000 } },
            { "history", new TypeGenre { Name = "history", BasePriceMultiplier = 10 } }
        };

        _mockTypeGenreRepository.Setup(repo => repo.GetByFilter(x => x.Name.Equals("tragedy")))
                                    .ReturnsAsync(new List<TypeGenre> { typeGenres.Values.FirstOrDefault(x => x.Name.Equals("tragedy")) });

        _mockTypeGenreRepository.Setup(repo => repo.GetByFilter(x => x.Name.Equals("comedy")))
                                    .ReturnsAsync(new List<TypeGenre> { typeGenres.Values.FirstOrDefault(x => x.Name.Equals("comedy")) });

        var plays = new Dictionary<string, Domain.Entities.Play>
        {
            { "hamlet", new Domain.Entities.Play { Name = "Hamlet", Lines = 4024, TypeGenre = typeGenres["tragedy"] } },
            { "as-like", new Domain.Entities.Play { Name = "As You Like It", Lines = 2670, TypeGenre = typeGenres["comedy"] } },
            { "othello", new Domain.Entities.Play { Name = "Othello", Lines = 3560, TypeGenre = typeGenres["tragedy"] } },
            { "henry-v", new Domain.Entities.Play { Name = "Henry V", Lines = 3227, TypeGenre = typeGenres["history"] } },
            { "john", new Domain.Entities.Play { Name = "King John", Lines = 2648, TypeGenre = typeGenres["history"] } },
            { "richard-iii", new Domain.Entities.Play { Name = "Richard III", Lines = 3718, TypeGenre = typeGenres["history"] } }
        };

        var invoice = new Domain.Entities.Invoice
        {
            Customer = "BigCo",
            Performances = new List<Domain.Entities.Performance>
            {
                new() { Play = plays["hamlet"], Audience = 55 },
                new() { Play = plays["as-like"], Audience = 35 },
                new() { Play = plays["othello"], Audience = 40 },
                new() { Play = plays["henry-v"], Audience = 20 },
                new() { Play = plays["john"], Audience = 39 },
                new() { Play = plays["henry-v"], Audience = 20 }
            }
        };

        StatementPrinterService statementPrinter = new StatementPrinterService(_mockTypeGenreRepository.Object, _iCustomerStatementRepository.Object);
        var result = await statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }

    #region [Calculate]

    [Fact]
    public async Task CalculateGenreTragedyCorrect()
    {
        var typeGenre = new TypeGenre
        {
            Name = "tragedy",
            BasePriceMultiplier = 10,
            MaxAudience = 30,
            ExtraFeePerAudience = 1000
        };

        var play = new Domain.Entities.Play { Name = "Hamlet", Lines = 4024, TypeGenre = typeGenre };
        var performance = new Domain.Entities.Performance { Play = play, Audience = 55 };
        var service = new StatementPrinterService(null, null);

        int lines = 3000;

        var result = await service.Calculate(play, performance, typeGenre, lines);

        Assert.True(result > 0);
    }

    [Fact]
    public async Task CalculateGenreComedyCorrect()
    {
        var typeGenre = new TypeGenre
        {
            Name = "comedy",
            BasePriceMultiplier = 10,
            MaxAudience = 20,
            ExtraFeePerAudience = 500,
            BaseFeePerAudience = 300,
            BonusFee = 10000
        };

        var play = new Domain.Entities.Play { Name = "As You Like It", Lines = 2670, TypeGenre = typeGenre };
        var performance = new Domain.Entities.Performance { Play = play, Audience = 35 };
        var service = new StatementPrinterService(null, null);

        int lines = 2000;

        var result = await service.Calculate(play, performance, typeGenre, lines);

        Assert.True(result > 0);
    }

    [Fact]
    public async Task CalculateGenreHistoryCorrect()
    {
        var typeGenres = new Dictionary<string, TypeGenre>
        {
            { "tragedy", new TypeGenre { Name = "tragedy", BasePriceMultiplier = 10, MaxAudience = 30, ExtraFeePerAudience = 1000 } },
            { "comedy", new TypeGenre { Name = "comedy", BasePriceMultiplier = 10, MaxAudience = 20, ExtraFeePerAudience = 500, BaseFeePerAudience = 300, BonusFee = 10000 } },
            { "history", new TypeGenre { Name = "history", BasePriceMultiplier = 10 } }
        };

        var typeGenre = new TypeGenre
        {
            Name = "history",
            BasePriceMultiplier = 10,
        };

        var play = new Domain.Entities.Play { Name = "Henry V", Lines = 3227, TypeGenre = typeGenre };
        var performance = new Domain.Entities.Performance { Play = play, Audience = 20 };

        _mockTypeGenreRepository.Setup(repo => repo.GetByFilter(x => x.Name.Equals("tragedy")))
                                    .ReturnsAsync(new List<TypeGenre> { typeGenres.Values.FirstOrDefault(x => x.Name.Equals("tragedy")) });

        _mockTypeGenreRepository.Setup(repo => repo.GetByFilter(x => x.Name.Equals("comedy")))
                                    .ReturnsAsync(new List<TypeGenre> { typeGenres.Values.FirstOrDefault(x => x.Name.Equals("comedy")) });

        var service = new StatementPrinterService(_mockTypeGenreRepository.Object, null);
        int lines = 2500;

        var result = await service.Calculate(play, performance, typeGenre, lines);

        Assert.True(result > 0);
    }

    #endregion
}