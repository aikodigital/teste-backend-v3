using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class AmountCalculationTests
    {
        private readonly ICalculateBaseAmountPerLine _calculateBaseAmount = new CalculateBaseAmountPerLine();
        private readonly ICalculateAdditionalValuePerPlayType _calculateAdditionalValue = new CalculateAdditionalValuePerPlayType();

        [Fact]
        public void CalculateBaseAmount()
        {
            var lines = 4024;
            var result = _calculateBaseAmount.CalculateBaseAmount(lines);

            Assert.Equal(400, result);
        }

        [Fact]
        public void CalculateAdditionalValue_ForAudienceAboveThreshold()
        {
            var audience = 55;
            var result = _calculateAdditionalValue.CalculateAdditionalValue(audience, 30, 5, 3, 2);

            Assert.Equal(190, result);
        }

        [Fact]
        public void CalculateAdditionalValue_ForAudienceBelowThreshold()
        {
            var audience = 20;
            var result = _calculateAdditionalValue.CalculateAdditionalValue(audience, 30, 5, 3, 2);

            Assert.Equal(40, result);
        }
    }
}
