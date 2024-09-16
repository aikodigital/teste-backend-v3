using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.UseCase.Statement;
using TheatricalPlayersRefactoringKata.Application.UseCase.Statement.Create;
using TheatricalPlayersRefactoringKata.Application.ViewModels;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Enum;
using TheatricalPlayersRefactoringKata.Services.PlayAmount;
using TheatricalPlayersRefactoringKata.Services.PlayVolumeCredits;
using TheatricalPlayersRefactoringKata.Tests.Comparators;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.UseCases;

public class CreateStatementUseCaseTest
{
    private readonly ICreateStatementUseCase _sut = new CreateStatementUseCase(
        playAmountService: new PlayAmountService(),
        playVolumeCreditsService: new PlayVolumeCreditsService()
    );

    [Fact]
    public void TestCreateStatement()
    {
        var input = new CreateStatementInput
        {
            Invoice = new InvoiceViewModel
            {
                Customer = "BigCo",
                Performances = new List<PerformanceViewModel>
                {
                    new() { PlayId = "hamlet", Audience = 55 },
                    new() { PlayId = "as-like", Audience = 35 },
                    new() { PlayId = "othello", Audience = 40 },
                    new() { PlayId = "henry-v", Audience = 20 },
                    new() { PlayId = "john", Audience = 39 },
                    new() { PlayId = "henry-v", Audience = 20 },
                }
            },
            Plays = new()
            {
                { "hamlet", new() { Name = "Hamlet", Lines = 4024, Type = PlayTypeEnum.Tragedy } },
                { "as-like", new() { Name = "As You Like It", Lines = 2670, Type = PlayTypeEnum.Comedy } },
                { "othello", new() { Name = "Othello", Lines = 3560, Type = PlayTypeEnum.Tragedy } },
                { "henry-v", new() { Name = "Henry V", Lines = 3227, Type = PlayTypeEnum.History } },
                { "john", new() { Name = "King John", Lines = 2648, Type = PlayTypeEnum.History } },
                { "richard-iii", new() { Name = "Richard III", Lines = 3718, Type = PlayTypeEnum.History } },
            }
        };

        var output = _sut.Execute(input);
        var statement = output.Statement;
        
        
        var expectedStatement = new StatementEntity(customer: "BigCo");
        expectedStatement.AddItem(name: "Hamlet", amountOwed: 650m, earnedCredits: 25, seats: 55);
        expectedStatement.AddItem(name: "As You Like It", amountOwed: 547m, earnedCredits: 12, seats: 35);
        expectedStatement.AddItem(name: "Othello", amountOwed: 456m, earnedCredits: 10, seats: 40);
        expectedStatement.AddItem(name: "Henry V", amountOwed: 705.4m, earnedCredits: 0, seats: 20);
        expectedStatement.AddItem(name: "King John", amountOwed: 931.6m, earnedCredits: 9, seats: 39);
        expectedStatement.AddItem(name: "Henry V", amountOwed: 705.4m, earnedCredits: 0, seats: 20);
        expectedStatement.Close(amountOwed: 3995.4m, earnedCredits: 56);
        
        Assert.Equal(expectedStatement.Customer, statement.Customer);
        Assert.Equal(expectedStatement.Items, statement.Items, new StatementItemEntityComparator());
        Assert.Equal(expectedStatement.AmountOwed, statement.AmountOwed);
        Assert.Equal(expectedStatement.EarnedCredits, statement.EarnedCredits);
    }
}