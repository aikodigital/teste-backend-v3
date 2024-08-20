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
            var amount01 = PerformancePrice.Price(900, 15, PlayType.Tragedy);
            var amount02 = PerformancePrice.Price(900, 20, PlayType.Tragedy);
            var amount03 = PerformancePrice.Price(900, 25, PlayType.Tragedy);
            var amount04 = PerformancePrice.Price(900, 30, PlayType.Tragedy);
            var amount05 = PerformancePrice.Price(900, 35, PlayType.Tragedy);

            var amount06 = PerformancePrice.Price(2000, 15, PlayType.Tragedy);
            var amount07 = PerformancePrice.Price(2000, 20, PlayType.Tragedy);
            var amount08 = PerformancePrice.Price(2000, 25, PlayType.Tragedy);
            var amount09 = PerformancePrice.Price(2000, 30, PlayType.Tragedy);
            var amount10 = PerformancePrice.Price(2000, 35, PlayType.Tragedy);

            var amount11 = PerformancePrice.Price(4500, 15, PlayType.Tragedy);
            var amount12 = PerformancePrice.Price(4500, 20, PlayType.Tragedy);
            var amount13 = PerformancePrice.Price(4500, 25, PlayType.Tragedy);
            var amount14 = PerformancePrice.Price(4500, 30, PlayType.Tragedy);
            var amount15 = PerformancePrice.Price(4500, 35, PlayType.Tragedy);

            string result = amount01.ToString() + " " + amount02.ToString() + " " +
                amount03.ToString() + " " + amount04.ToString() + " " + amount05.ToString() + "\n" +
                amount06.ToString() + " " + amount07.ToString() + " " +
                amount08.ToString() + " " + amount09.ToString() + " " + amount10.ToString() + "\n" +
                amount11.ToString() + " " + amount12.ToString() + " " +
                amount13.ToString() + " " + amount14.ToString() + " " + amount15.ToString();

            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void ComedyUseCaseTest()
        {
            var amount01 = PerformancePrice.Price(900, 15, PlayType.Comedy);
            var amount02 = PerformancePrice.Price(900, 20, PlayType.Comedy);
            var amount03 = PerformancePrice.Price(900, 25, PlayType.Comedy);
            var amount04 = PerformancePrice.Price(900, 30, PlayType.Comedy);
            var amount05 = PerformancePrice.Price(900, 35, PlayType.Comedy);

            var amount06 = PerformancePrice.Price(2000, 15, PlayType.Comedy);
            var amount07 = PerformancePrice.Price(2000, 20, PlayType.Comedy);
            var amount08 = PerformancePrice.Price(2000, 25, PlayType.Comedy);
            var amount09 = PerformancePrice.Price(2000, 30, PlayType.Comedy);
            var amount10 = PerformancePrice.Price(2000, 35, PlayType.Comedy);

            var amount11 = PerformancePrice.Price(4500, 15, PlayType.Comedy);
            var amount12 = PerformancePrice.Price(4500, 20, PlayType.Comedy);
            var amount13 = PerformancePrice.Price(4500, 25, PlayType.Comedy);
            var amount14 = PerformancePrice.Price(4500, 30, PlayType.Comedy);
            var amount15 = PerformancePrice.Price(4500, 35, PlayType.Comedy);

            string result = amount01.ToString() + " " + amount02.ToString() + " " +
                amount03.ToString() + " " + amount04.ToString() + " " + amount05.ToString() + "\n" +
                amount06.ToString() + " " + amount07.ToString() + " " +
                amount08.ToString() + " " + amount09.ToString() + " " + amount10.ToString() + "\n" +
                amount11.ToString() + " " + amount12.ToString() + " " +
                amount13.ToString() + " " + amount14.ToString() + " " + amount15.ToString();

            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void HistoryUseCaseTest()
        {
            var amount01 = PerformancePrice.Price(900, 15, PlayType.History);
            var amount02 = PerformancePrice.Price(900, 20, PlayType.History);
            var amount03 = PerformancePrice.Price(900, 25, PlayType.History);
            var amount04 = PerformancePrice.Price(900, 30, PlayType.History);
            var amount05 = PerformancePrice.Price(900, 35, PlayType.History);

            var amount06 = PerformancePrice.Price(2000, 15, PlayType.History);
            var amount07 = PerformancePrice.Price(2000, 20, PlayType.History);
            var amount08 = PerformancePrice.Price(2000, 25, PlayType.History);
            var amount09 = PerformancePrice.Price(2000, 30, PlayType.History);
            var amount10 = PerformancePrice.Price(2000, 35, PlayType.History);

            var amount11 = PerformancePrice.Price(4500, 15, PlayType.History);
            var amount12 = PerformancePrice.Price(4500, 20, PlayType.History);
            var amount13 = PerformancePrice.Price(4500, 25, PlayType.History);
            var amount14 = PerformancePrice.Price(4500, 30, PlayType.History);
            var amount15 = PerformancePrice.Price(4500, 35, PlayType.History);

            string result = amount01.ToString() + " " + amount02.ToString() + " " +
                amount03.ToString() + " " + amount04.ToString() + " " + amount05.ToString() + "\n" +
                amount06.ToString() + " " + amount07.ToString() + " " +
                amount08.ToString() + " " + amount09.ToString() + " " + amount10.ToString() + "\n" +
                amount11.ToString() + " " + amount12.ToString() + " " +
                amount13.ToString() + " " + amount14.ToString() + " " + amount15.ToString();

            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void CreditsTest()
        {
            //int audience, bool isComedy
            var credits1 = PerformancePrice.Credits(29, false);
            var credits2 = PerformancePrice.Credits(30, false);
            var credits3 = PerformancePrice.Credits(31, false);
            var credits4 = PerformancePrice.Credits(45, false);

            var credits5 = PerformancePrice.Credits(29, true);
            var credits6 = PerformancePrice.Credits(30, true);
            var credits7 = PerformancePrice.Credits(31, true);
            var credits8 = PerformancePrice.Credits(45, true);

            string result = credits1.ToString() + " " + credits2.ToString() + " " + credits3.ToString() + " " + credits4.ToString() + "\n" +
                            credits5.ToString() + " " + credits6.ToString() + " " + credits7.ToString() + " " + credits8.ToString();
            Approvals.Verify(result);
        }
    }
}
