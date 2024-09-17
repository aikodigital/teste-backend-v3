namespace TS.Application.Invoices.Queries.GetInvoices.Response
{
    public class GetInvoicesResponse
    {
        public long Id { get; set; }
        public DateTime CreationAt { get; set; }
        public long CustomerId { get; set; }
        public long PlayId { get; set; }
        public decimal LoyaltyCredit { get; set; }
    }
}