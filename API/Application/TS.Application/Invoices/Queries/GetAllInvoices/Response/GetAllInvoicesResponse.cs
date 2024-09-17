namespace TS.Application.Invoices.Queries.GetAllInvoices.Response
{
    public class GetAllInvoicesResponse
    {
        public long Id { get; set; }
        public DateTime CreationAt { get; set; }
        public long CustomerId { get; set; }
        public long PlayId { get; set; }
        public decimal LoyaltyCredit { get; set; }
    }
}