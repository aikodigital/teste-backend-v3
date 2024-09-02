using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.EventHandlers
{
    public class InvoiceCreatedEvent : DomainEvent
    {
        public InvoiceCreatedEvent(Invoice invoice)
        {
            Invoice = invoice;
        }

        public Invoice Invoice { get; }
    }
}
