using Aplication.DTO;
using Aplication.Mappings;
using AutoMapper;
using CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entity;

namespace AplicationTest
{
    public class TesteMappingInvoice
    {
        [Fact]
        public void TestInvoiceMapping()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            var invoiceDto = new InvoiceDto
            {
                Id = Guid.NewGuid(),
                Customer = "Luiz Farias",
                Performances = new List<PerformanceDto>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Play = new PlayDto { Id = Guid.NewGuid(), Name = "Hamlet", Lines = 120, Type = PlayType.tragedy },
                        Audience = 150
                    }
                }
            };

            var invoice = mapper.Map<Invoice>(invoiceDto);
            Assert.NotNull(invoice);
            Assert.Equal(invoiceDto.Customer, invoice.Customer);
        }
    }
}
