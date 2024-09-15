using MediatR;

namespace TS.Application.Invoices.Commands.UpdateInvoices.Request
{
    public class UpdateInvoicesRequest : IRequest
    {
        public long Id { get; set; }
        public DateTime CreationAt { get; set; }
        public long CustomerId { get; set; }
        public long PlayId { get; set; }
        public decimal LoyaltyCredit { get; set; }
    }
}