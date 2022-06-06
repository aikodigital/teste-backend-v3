using System;
using System.Collections.Generic;
using System.Globalization;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Contracts;
using TheatricalPlayersRefactoringKata.Performances;
using TheatricalPlayersRefactoringKata.Printers;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public partial class StatementPrinterTests
{
    public static IEnumerable<object[]> GetInvoice()
    {
        IPlay hamlet = new Play("Hamlet", 4024),
            asLike = new Play("As You Like It", 2670),
            othello = new Play("Othello", 3560),
            henryV = new Play("Henry V", 3227),
            john = new Play("King John", 2648);


        Invoice invoice = new Invoice(
            "BigCo",
            new List<IPerformance>
            {
                new TragedyPerformance(hamlet, 55),
                new ComedyPerformance(asLike, 35),
                new TragedyPerformance(othello, 40),
                new HistoryPerformance(henryV, 20),
                new HistoryPerformance(john, 39),
                new HistoryPerformance(henryV, 20)
            }
        );

        yield return new object[] { invoice, CultureInfo.CreateSpecificCulture("en-US") };
    }
}
