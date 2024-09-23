using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.enums;
using AutoMapper;
using ApprovalTests.Namers;
namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementTests
{
    private readonly IMapper mapper;
    public  StatementTests()
    {
        mapper = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapping()))
        .CreateMapper();
    }

    public bool SerializeStament_IsNull()
    {
        StatementToXml statement = null;

        Assert.Throws<ArgumentNullException>(() => Services.Serialize.SerializeStatement.SerializeToXMl(statement));

        return true;
    }
}

public class StatementGeneratorTests
{
        private readonly StatementGenerator _statementGenerator;
        private readonly Invoice _invoice;
        private readonly Dictionary<string, Play> _plays;

        public StatementGeneratorTests()
        {
            _plays = new Dictionary<string, Play>()
            {
                { "hamlet", new  Play("Hamlet", 4024, PlayType.tragedy) },
                { "as-like", new Play("As You Like It", 2670, PlayType.comedy) },
                { "othello", new Play("Othello", 3560, PlayType.tragedy) },
                { "henry-v", new Play("Henry V", 3227, PlayType.history) } 
            };

 

            _invoice = new Invoice("BigCo", new List<Performance>()
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                new Performance("henry-v", 10)
            });

            var statementInput = new StatementInput(_invoice,_plays);

            _statementGenerator = new StatementGenerator(statementInput);
        }

        [Fact]
        public void GenerateStatement_CalculatesTotalAmountOwedCorrectly()
        {
            var statement = _statementGenerator.GenerateStatement();
            statement.AmountOwed.Equals(1735.00); 
        }

        [Fact]
        public void GenerateStatement_CalculatesTotalEarnedCreditsCorrectly()
        {
            var statement = _statementGenerator.GenerateStatement();
            statement.EarnedCredits.Equals(47);
        }

        [Fact]
        public void GenerateStatement_SetsCustomerNameCorrectly()
        {
            var statement = _statementGenerator.GenerateStatement();
            statement.Customer.Equals("BigCo");
        }

        [Theory]
        [InlineData(PlayType.tragedy, 55, 1000, 650.00, 25)] 
        [InlineData(PlayType.comedy, 35, 1000, 580.00, 7)] 
        [InlineData(PlayType.history, 10, 1000, 600.00, 0)] 
        public void CalculateAmount_CalculatesCorrectAmountForDifferentPlayTypes(PlayType playType, int audience, int lines, decimal expectedAmount, int expectedCredits)
        {
            var play = new Play("test", lines,playType);
            var amount = _statementGenerator.CalculateAmount(playType, audience, lines);
            var credits = _statementGenerator.CalculateEarnedCredits(audience, playType);

            amount.Equals(expectedAmount);
            credits.Equals(expectedCredits);
        }
}
