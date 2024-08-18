using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    class GenerateInvoiceReportsXML
    {
        [Fact]
        private GenerateInvoiceReportsXML CreateUseCase(Invoice invoice)
        {
            var repository = new InvoiceReadOnlyReposBuilder().FilterByMonth(user, expenses).Build();
      
            return new GenerateInvoiceReportsXML(repository, loggedUser);
        }
    }
}
