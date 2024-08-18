using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class CollectionChargeTests
    {
        [Fact]
        public void CalculateBillingAudience_Less20()
        {
            // Arrange
            var performance = new Performance("as-like", 15);
            var play = new Play("As You Like It", 2670, "comedy");
            var chargeStrategy = new CollectionChargy();

            // Act
            var result = chargeStrategy.CalculateBilling(performance, play);

            // Assert
            Assert.Equal(7950, result);
        }

        [Fact]
        public void CalculateAudience20()
        {
            // Arrange
            var performance = new Performance("as-like", 35);
            var play = new Play("As You Like It", 2670, "comedy");
            var chargeStrategy = new CollectionChargy();

            // Act
            var result = chargeStrategy.CalculateBilling(performance, play);

            // Assert
            Assert.Equal(19850, result);
        }
    }
}
