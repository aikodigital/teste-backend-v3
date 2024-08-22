using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class ModelsTests
    {
        [Fact]
        public void CreatePlayNullArgumentToLinesThrowArgumentException()
        {
            // arrange

            string name = "bela e a fera";
            int lines = -4;
            var type = EPlayType.Tragedy;

            // act

            var exception = Assert.Throws<ArgumentException>(() => new Play(name, lines, type));

            // assert

            Assert.Equal("Lines must be greater than 0", exception.Message);
        }

        [Fact]
        public void CreatePlayNullArgumentToNameThrowArgumentException()
        {
            // arrange

            string name = "it";
            int lines = 1;
            var type = EPlayType.Tragedy;

            // act

            var exception = Assert.Throws<ArgumentException>(() => new Play(name, lines, type));

            // assert

            Assert.Equal("Name must be greater than 3 characters", exception.Message);
        }

        [Fact]
        public void CreatePlayNullArgumentToTypeThrowArgumentException()
        {
            // arrange

            string name = "itAcoisa";
            int lines = 1;
            var type = new EPlayType();

            // act

            var exception = Assert.Throws<ArgumentException>(() => new Play(name, lines, type));

            // assert

            Assert.Equal("Play Type must be Tragedy, Comedy or History", exception.Message);
        }

        [Fact]
        public void CreateCustomerNullArgumentToNameThrowArgumentException()
        {
            // arrange

            string name = "it";
            double credits = 1;

            // act

            var exception = Assert.Throws<ArgumentException>(() => new Customer(name, credits));

            // assert

            Assert.Equal("Name must be greater than 3 characters", exception.Message);
        }

        [Fact]
        public void CreateCustomerNullArgumentToCreditsThrowArgumentException()
        {
            // arrange

            string name = "itAcoisa";
            int credits = -1;

            // act

            var exception = Assert.Throws<ArgumentException>(() => new Customer(name, credits));

            // assert

            Assert.Equal("Credits must be greater than 0", exception.Message);
        }

        [Fact]
        public void CreatePerformanceNullArgumentToPlayThrowArgumentException()
        {
            // arrange

            int audience = 1;

            // act

            var exception = Assert.Throws<ArgumentException>(() => new Performance(null, audience));

            // assert

            Assert.Equal("Play cannot be null", exception.Message);
        }

        [Fact]
        public void CreatePerformanceNullArgumentToAudienceThrowArgumentException()
        {
            // arrange
            var play = new Play("It A Coisa", 14, EPlayType.Tragedy);
            int audience = -1;

            // act

            var exception = Assert.Throws<ArgumentException>(() => new Performance(play, audience));

            // assert

            Assert.Equal("Audiance must be greater than 0", exception.Message);
        }

        [Fact]
        public void CreateInvoiceNullArgumentToCustomerThrowArgumentException()
        {
            // arrange
            var Hamlet = new Play("Hamlet", 4024, Enums.EPlayType.Tragedy);
            var AsYouLikeIt = new Play("As You Like It", 2670, Enums.EPlayType.Comedy);

            var performances = new List<Performance>
        {
            new Performance(Hamlet, 55),
            new Performance(AsYouLikeIt, 35)
        };

            // act

            var exception = Assert.Throws<ArgumentException>(() => new Invoice(null, performances));

            // assert

            Assert.Equal("Customer cannot be null", exception.Message);
        }

        [Fact]
        public void CreateInvoiceNullArgumentToPerformanceThrowArgumentException()
        {
            // arrange
            var customer = new Customer("Customer test", 0);

            // act

            var exception = Assert.Throws<ArgumentException>(() => new Invoice(customer, null));

            // assert

            Assert.Equal("Performance list cannot be null", exception.Message);
        }
    }
}
