namespace TS.Presentation.ViewModels
{
    public class UpdateInvoicesViewModel
    {
        public long Id { get; set; }
        public DateTime CreationAt { get; set; }
        public long CustomerId { get; set; }
        public long PlayId { get; set; }
        public decimal LoyaltyCredit { get; set; }
    }
}