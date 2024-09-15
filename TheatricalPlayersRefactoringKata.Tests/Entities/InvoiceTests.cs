using ApprovalTests;
using ApprovalTests.Reporters;
using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Validation;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Entities
{
    public class InvoiceTests : IDisposable
    {
        private readonly List<Performance> _performances;

        public InvoiceTests()
        {
            _performances = new List<Performance>
            {
                new(new Play("Hamlet", 4024, PlayTypeEnum.Tragedy), 55),
                new(new Play("As You Like It", 2670, PlayTypeEnum.Comedy), 35),
                new(new Play("Henry V", 3227, PlayTypeEnum.History),20)
            };
        }

        [Fact]
        public void CreateInvoiceWithValidValues()
        {
            var customer = "BigCo";

            Invoice newInvoice = new Invoice(customer, _performances);

            Assert.Equal(customer, newInvoice.Customer);
            Assert.Contains(newInvoice.Performances, (performance) => _performances.Contains(performance));
        }

        [Fact]
        public void CreateInvoiceWithInalidCustomer()
        {
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Invoice("", _performances));

            Assert.Equal("Invalid customer. Customer is required.", exception.Message);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void PrintInvoiceStatement()
        {
            var customer = "BigCo";

            Invoice newInvoice = new Invoice(customer, _performances);

            string invoiceStatement = newInvoice.PrintInvoiceStatement();

            Approvals.Verify(invoiceStatement);
        }

        public void Dispose()
        {
            _performances.Clear();
        }
    }
}
