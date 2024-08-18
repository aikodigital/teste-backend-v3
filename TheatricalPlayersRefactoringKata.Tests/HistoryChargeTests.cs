using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata;
using Xunit;


namespace TheatricalPlayersRefactoringKata.Tests
{
    public class HistoryChargeTests
    {
        [Fact]
        public void CalculateAudience_Less30()
        {
            // Arrange
            var performance = new Performance("henry-v", 20);
            var play = new Play("Henry V", 3227, "history");
            var chargeStrategy = new HistoryCharge();

            // Act
            var result = chargeStrategy.CalculateBilling(performance, play);

            // Assert
            Assert.Equal(32270, result);
        }

        [Fact]
        public void CalculateAudience30()
        {
            // Arrange
            var performance = new Performance("henry-v", 40);
            var play = new Play("Henry V", 3227, "history");
            var chargeStrategy = new HistoryCharge();

            // Act
            var result = chargeStrategy.CalculateBilling(performance, play);

            // Assert
            Assert.Equal(64540, result);
        }
    }
}
