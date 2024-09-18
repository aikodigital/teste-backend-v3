using System;
using System.Collections.Generic;
using System.Linq;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Validation;
using TheatricalPlayersRefactoringKata.Domain.ValueObjects;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Entities
{
    public class StatementTests : IDisposable
    {
        private readonly List<StatementItem> _statementItems;

        public StatementTests()
        {
            _statementItems = new List<StatementItem>
            {
                StatementItem.Create(650,25,55),
                StatementItem.Create(547,12,35),
                StatementItem.Create(456,10,40),
                StatementItem.Create(705.4M,0,20),
                StatementItem.Create(931.6M,9,39)
            };
        }

        [Fact]
        public void CreateStatementWithValidValues()
        {
            var customer = "BigCo";
            Statement statement = Statement.Create(customer, _statementItems);

            Assert.NotNull(statement);
            Assert.Contains(statement.Items, (item) => _statementItems.Contains(item));
            Assert.Equal(customer, statement.Customer);
            Assert.Equal(statement.AmountOwed, _statementItems.Sum(x => x.AmountOwed));
            Assert.Equal(statement.EarnedCredits, _statementItems.Sum(x => x.EarnedCredits));
        }

        [Fact]
        public void CreateStatementWithInvalidCustomer()
        {
            var exception = Assert.Throws<DomainExceptionValidation>(() => Statement.Create("", _statementItems));

            Assert.Equal("Invalid customer. Customer is required.", exception.Message);
        }

        public void Dispose()
        {
            _statementItems.Clear();
        }
    }
}
