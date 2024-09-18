using System;
using System.Collections.Generic;
using System.Globalization;
using ApprovalTests;
using ApprovalTests.Reporters;
using Domain.DTOs;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
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

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));
        plays.Add("henry-v", new Play("Henry V", 3227, "history"));
        plays.Add("john", new Play("King John", 2648, "history"));
        plays.Add("richard-iii", new Play("Richard III", 3718, "history"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                new Performance("henry-v", 20),
                new Performance("john", 39),
                new Performance("henry-v", 20)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));
        plays.Add("henry-v", new Play("Henry V", 3227, "history"));
        plays.Add("john", new Play("King John", 2648, "history"));
        plays.Add("richard-iii", new Play("Richard III", 3718, "history"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                new Performance("henry-v", 20),
                new Performance("john", 39),
                new Performance("henry-v", 20)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Xml(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestReportStatementExample()
    {
        CultureInfo cultureInfo = new CultureInfo("en-US");
        IList<TheaterPlayDTO> theaterplays = new List<TheaterPlayDTO>();
        theaterplays.Add(new TheaterPlayDTO
        {
            Play = new PlayDTO
            {
                Id = 1,
                Name = "Hamlet",
                Lines = 4024,
                Type = "tragedy"
            },
            PlayId = 1,
            Name = "Hamlet",
        });
        theaterplays.Add(new TheaterPlayDTO
        {
            Play = new PlayDTO
            {
                Id = 2,
                Name = "As You Like It",
                Lines = 2670,
                Type = "comedy"
            },
            PlayId = 2,
            Name = "as-like",
        });
        theaterplays.Add(new TheaterPlayDTO
        {
            Play = new PlayDTO
            {
                Id = 3,
                Name = "Othello",
                Lines = 3560,
                Type = "tragedy"
            },
            PlayId = 3,
            Name = "othello",
        });
        theaterplays.Add(new TheaterPlayDTO
        {
            Play = new PlayDTO
            {
                Id = 4,
                Name = "Henry V",
                Lines = 3227,
                Type = "history"
            },
            PlayId = 4,
            Name = "henry-v",
        });
        theaterplays.Add(new TheaterPlayDTO
        {
            Play = new PlayDTO
            {
                Id = 5,
                Name = "King John",
                Lines = 2648,
                Type = "history"
            },
            PlayId = 5,
            Name = "john",
        });
        theaterplays.Add(new TheaterPlayDTO
        {
            Play = new PlayDTO
            {
                Id = 6,
                Name = "Richard III",
                Lines = 3718,
                Type = "history"
            },
            PlayId = 6,
            Name = "richard-iii",
        });

        var Invoice = new InvoiceDTO
        {
            Id = 1,
            Customer = "BigCo",
            Performances = new List<PerformanceDTO>
            {
                new PerformanceDTO
                {
                    PlayId = 1,
                    Audience = 55
                },
                new PerformanceDTO
                {
                    PlayId = 2,
                    Audience = 35
                },
                new PerformanceDTO
                {
                    PlayId = 3,
                    Audience = 40
                },
                new PerformanceDTO
                {
                    PlayId = 4,
                    Audience = 20
                },
                new PerformanceDTO
                {
                    PlayId = 5,
                    Audience = 39
                },
                new PerformanceDTO
                {
                    PlayId = 4,
                    Audience = 20
                }
            }
        };


        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Report(Invoice,theaterplays);


        string text = "";
        foreach (var item in result)
        {
            if (string.IsNullOrEmpty(text))
            {
                text = string.Format("Statement for {0}\n", item.Invoice.Customer);
            }
            text += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", item.Play.Name, item.Amount, item.Seats);
        }

        Approvals.Verify(text);
    }


    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestReportCreditStatementExample()
    {
        CultureInfo cultureInfo = new CultureInfo("en-US");
        IList<TheaterPlayDTO> theaterplays = new List<TheaterPlayDTO>();
        theaterplays.Add(new TheaterPlayDTO
        {
            Play = new PlayDTO
            {
                Id = 1,
                Name = "Hamlet",
                Lines = 4024,
                Type = "tragedy"
            },
            PlayId = 1,
            Name = "Hamlet",
        });
        theaterplays.Add(new TheaterPlayDTO
        {
            Play = new PlayDTO
            {
                Id = 2,
                Name = "As You Like It",
                Lines = 2670,
                Type = "comedy"
            },
            PlayId = 2,
            Name = "as-like",
        });
        theaterplays.Add(new TheaterPlayDTO
        {
            Play = new PlayDTO
            {
                Id = 3,
                Name = "Othello",
                Lines = 3560,
                Type = "tragedy"
            },
            PlayId = 3,
            Name = "othello",
        });
        theaterplays.Add(new TheaterPlayDTO
        {
            Play = new PlayDTO
            {
                Id = 4,
                Name = "Henry V",
                Lines = 3227,
                Type = "history"
            },
            PlayId = 4,
            Name = "henry-v",
        });
        theaterplays.Add(new TheaterPlayDTO
        {
            Play = new PlayDTO
            {
                Id = 5,
                Name = "King John",
                Lines = 2648,
                Type = "history"
            },
            PlayId = 5,
            Name = "john",
        });
        theaterplays.Add(new TheaterPlayDTO
        {
            Play = new PlayDTO
            {
                Id = 6,
                Name = "Richard III",
                Lines = 3718,
                Type = "history"
            },
            PlayId = 6,
            Name = "richard-iii",
        });

        var Invoice = new InvoiceDTO
        {
            Id = 1,
            Customer = "BigCo",
            Performances = new List<PerformanceDTO>
            {
                new PerformanceDTO
                {
                    PlayId = 1,
                    Audience = 55
                },
                new PerformanceDTO
                {
                    PlayId = 2,
                    Audience = 35
                },
                new PerformanceDTO
                {
                    PlayId = 3,
                    Audience = 40
                },
                new PerformanceDTO
                {
                    PlayId = 4,
                    Audience = 20
                },
                new PerformanceDTO
                {
                    PlayId = 5,
                    Audience = 39
                },
                new PerformanceDTO
                {
                    PlayId = 4,
                    Audience = 20
                }
            }
        };


        StatementPrinter statementPrinter = new StatementPrinter();
        var report = statementPrinter.Report(Invoice, theaterplays);
        var result = statementPrinter.ReportCredits(report);
        var text = String.Format(cultureInfo, "Amount owed is {0:C}\n", result.AmountTotal);
        text += String.Format("You earned {0} credits", result.Credits);

        Approvals.Verify(text);
    }
}
