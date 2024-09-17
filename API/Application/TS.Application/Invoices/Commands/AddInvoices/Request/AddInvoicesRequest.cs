using MediatR;

namespace TS.Application.Invoices.Commands.AddInvoices.Request
{
    public class AddInvoicesRequest : IRequest
    {
        public long CustomerId { get; set; }
        public decimal Seats { get; set; }
        public virtual IEnumerable<AddInvoicePerformances> Performances { get; set; } = [];
    }

    public class AddInvoicePerformances
    {
        public long PlayId { get; set; }
        public int Audience { get; set; }
    }
}