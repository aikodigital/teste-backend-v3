using ApprovalTests;
using ApprovalTests.Reporters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Services.InvoicePrice;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class PerformancePriceTests
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TragedyUseCaseTest()
        {
            var thisAmount = PerformancePrice.Price(900, 21, PlayType.Tragedy);
            Approvals.Verify(thisAmount);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void ComedyUseCaseTest()
        {
            var thisAmount = PerformancePrice.Price(900, 21, PlayType.Comedy);
            Approvals.Verify(thisAmount);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void HistoryUseCaseTest()
        {
            var thisAmount = PerformancePrice.Price(900, 21, PlayType.History);
            Approvals.Verify(thisAmount);
        }
    }
}
