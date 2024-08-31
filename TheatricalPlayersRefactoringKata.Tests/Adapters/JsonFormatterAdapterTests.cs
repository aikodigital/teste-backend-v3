using ApprovalTests;
using ApprovalTests.Reporters;
using AutoMapper;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.Adapters;
using TheatricalPlayersRefactoringKata.Application.Mappings;
using TheatricalPlayersRefactoringKata.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Adapters
{
    public class JsonFormatterAdapterTests
    {
        private readonly IMapper _mapper;

        public JsonFormatterAdapterTests()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void Format_ShouldReturnFormattedStatement()
        {
            // Configura um cenário onde uma declaração é formatada em JSON.
            // Verifica se a declaração formatada está correta.

            // Arrange
            var formatter = new JsonFormatterAdapter(_mapper);
            var statement = new Statement
            {
                Customer = "John Doe",
                Items = new List<StatementItem>
                    {
                        new StatementItem { Name = "Hamlet", AmountOwed = 400.00m, Seats = 55 },
                        new StatementItem { Name = "As You Like It", AmountOwed = 300.00m, Seats = 35 },
                        new StatementItem { Name = "Othello", AmountOwed = 500.00m, Seats = 40 }
                    },
                TotalAmountOwed = 1200.00m,
                TotalEarnedCredits = 30
            };

            // Act
            var result = formatter.Format(statement);

            // Assert
            Approvals.Verify(result);
        }
    }
}

