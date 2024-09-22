using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Service;

public class StatementPrinterServiceTests
{
    private readonly static string Hamlet = "hamlet";
    private readonly static string AsLike = "as-like";
    private readonly static string Othello = "othello";
    private readonly static string HenryV = "henry-v";
    private readonly static string John = "john";
    private readonly static string RichardIii = "richard-iii";
    private readonly static string Customer = "BigCo";

    private readonly static Dictionary<string, Play> PlayMap = new()
    {
        { Hamlet, new () { Name = "Hamlet", Lines = 4024, Type = Gender.Tragedy} },
        { AsLike, new () { Name = "As You Like It", Lines = 2670, Type = Gender.Comedy} },
        { Othello, new () { Name = "Othello", Lines = 3560, Type = Gender.Tragedy} },
        { HenryV, new () { Name = "Henry V", Lines = 3227, Type = Gender.History} },
        { John, new () { Name = "King John", Lines = 2648, Type = Gender.History} },
        { RichardIii, new () { Name = "Henry V", Lines = 3227, Type = Gender.History} }
        //{ RichardIii, new () { Name = "Richard III", Lines = 3718, Type = Gender.History } }
    };

    private readonly static List<Performance> Performances = new()
    {
        new () { PlayId = Hamlet, Audience = 55 },
        new () { PlayId = AsLike, Audience = 35 },
        new () { PlayId = Othello, Audience = 40 },
        new () { PlayId = HenryV, Audience = 20 },
        new () { PlayId = John, Audience = 39 },
        new () { PlayId = RichardIii, Audience = 20 },
        //new () { PlayId = RichardIii, Audience = 20 }
    };

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy() =>
        RunTest(
            FileExtension.Txt,
            Performances
                .Take(3)
                .ToList(),
            PlayMap
                .Take(3)
                .ToDictionary(p => p.Key, p => p.Value));

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample() =>
        RunTest(FileExtension.Xml, Performances, PlayMap);

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample() =>
        RunTest(FileExtension.Txt, Performances, PlayMap);

    private static void RunTest(FileExtension extension, List<Performance> performances, Dictionary<string, Play> plays)
    {
        var invoice = new Invoice
        {
            Customer = Customer,
            Performances = performances
        };

        var result = StatementPrinterService.Print(extension, invoice, plays);
        Approvals.Verify(result);
    }
}
