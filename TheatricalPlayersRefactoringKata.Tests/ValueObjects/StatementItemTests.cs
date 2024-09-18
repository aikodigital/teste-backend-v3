using TheatricalPlayersRefactoringKata.Domain.Validation;
using TheatricalPlayersRefactoringKata.Domain.ValueObjects;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.ValueObjects
{
    public class StatementItemTests
    {
        [Fact]
        public void CreateStatementItemWithValidValues()
        {
            decimal amountOwed = 650;
            int earnedCredits = 25;
            int seats = 55;

            StatementItem item = StatementItem.Create(amountOwed, earnedCredits, seats);

            Assert.Equal(amountOwed, item.AmountOwed);
            Assert.Equal(earnedCredits, item.EarnedCredits);
            Assert.Equal(seats, item.Seats);
        }

        [Fact]
        public void CreateStatementItemWithInvalidAmountOwed()
        {
            decimal amountOwed = -1;
            int earnedCredits = 25;
            int seats = 55;

            var exception = Assert.Throws<DomainExceptionValidation>(() => StatementItem.Create(amountOwed, earnedCredits, seats));

            Assert.Equal("Invalid amount owed value. Amount owed value must be higher or equal than 0.", exception.Message);
        }

        [Fact]
        public void CreateStatementItemWithInvalidEarnedCredits()
        {
            decimal amountOwed = 650;
            int earnedCredits = -1;
            int seats = 55;

            var exception = Assert.Throws<DomainExceptionValidation>(() => StatementItem.Create(amountOwed, earnedCredits, seats));

            Assert.Equal("Invalid earned credits value. Earned credits value must be higher or equal than 0.", exception.Message);
        }

        [Fact]
        public void CreateStatementItemWithInvalidSeats()
        {
            decimal amountOwed = 650;
            int earnedCredits = 25;
            int seats = -1;

            var exception = Assert.Throws<DomainExceptionValidation>(() => StatementItem.Create(amountOwed, earnedCredits, seats));

            Assert.Equal("Invalid seats value. Seats value must be higher or equal than 0.", exception.Message);
        }

        [Fact]
        public void CompareEqualsStatementItemValues()
        {
            StatementItem itemOne = StatementItem.Create(650, 25, 55);
            StatementItem itemTwo = StatementItem.Create(650, 25, 55);

            Assert.True(itemOne.Equals(itemTwo));
            Assert.True(itemOne.GetHashCode() == itemTwo.GetHashCode());
        }

        [Fact]
        public void CompareNotEqualsStatementItemValues()
        {
            StatementItem itemOne = StatementItem.Create(650, 25, 55);
            StatementItem itemTwo = StatementItem.Create(650, 25, 50);

            Assert.False(itemOne.Equals(itemTwo));
            Assert.False(itemOne.GetHashCode() == itemTwo.GetHashCode());
        }
    }
}
