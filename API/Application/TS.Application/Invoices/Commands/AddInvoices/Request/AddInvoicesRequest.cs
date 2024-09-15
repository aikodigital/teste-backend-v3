using MediatR;

namespace TS.Application.Invoices.Commands.AddInvoices.Request
{
    public class AddInvoicesRequest : IRequest
    {
        public long CustomerId { get; set; }
        public long PlayId { get; set; }
        public decimal LoyaltyCredit { get; set; }
    }
}