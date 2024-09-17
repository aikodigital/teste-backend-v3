using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class CreditCalculationTests
    {
        private readonly ICalculateCreditAudience _calculateCreditAudience = new CalculateCreditAudience();

        [Fact]
        public void CalculateCredits_ForAudienceAboveThreshold()
        {
            var audience = 55;
            var creditSetting = new InvoiceCreditSettings(1, 1, 30, 5m);

            var result = _calculateCreditAudience.CalculateCredit(audience, creditSetting);

            Assert.Equal(36, result);
        }

        [Fact]
        public void CalculateCredits_ForAudienceBelowThreshold()
        {
            var audience = 20;
            var creditSetting = new InvoiceCreditSettings(1, 1, 30, 5m);

            var result = _calculateCreditAudience.CalculateCredit(audience, creditSetting);

            Assert.Equal(4, result);
        }
    }
}
