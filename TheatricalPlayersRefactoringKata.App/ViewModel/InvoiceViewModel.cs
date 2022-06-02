namespace TheatricalPlayersRefactoringKata.App.ViewModel
{
    public class InvoiceViewModel
    {
        public long Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public decimal CustomerId { get; set; }
        public decimal TotalAmout { get; set; }
        public decimal TotalCredits { get; set; }
    }
}