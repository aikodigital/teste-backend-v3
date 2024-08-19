using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class GenerateInvoiceReportsXML
    {
        [Fact]
        public async Task Success()
        {

            var invoices = InvoiceBuilder.Build();
            var useCase = GenerateReportsValidation(invoices);

            var result = await useCase.GenerateReport(DateOnly.FromDateTime(DateTime.Today));

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        private GenerateInvoiceReportsXML CreateUseCase(Invoice invoice)
        {
            var repository = new InvoiceReadOnlyReposBuilder().GenerateReport(invoice,"BigCo").Build();
      
            return new GenerateInvoiceReportsXML(repository);
        }
    }
}
