using System.Collections.Generic;
using Xunit;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class InvoiceTests
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            var customer = "BigCo";
            var performances = new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35)
            };

            var invoice = new Invoice(customer, performances);

            Assert.Equal(customer, invoice.Customer);
            Assert.Equal(performances, invoice.Performances);
        }

        [Fact]
        public void SetCustomer_ShouldUpdateCustomerProperty()
        {
            var invoice = new Invoice("OldCo", new List<Performance>());

            invoice.Customer = "NewCo";

            Assert.Equal("NewCo", invoice.Customer);
        }

        [Fact]
        public void SetPerformances_ShouldUpdatePerformancesProperty()
        {
            var invoice = new Invoice("BigCo", new List<Performance>());

            var newPerformances = new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35)
            };
            invoice.Performances = newPerformances;

            Assert.Equal(newPerformances, invoice.Performances);
        }
    }
}