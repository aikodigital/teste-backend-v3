namespace TheatricalPlayersRefactoringKata.App.Model.Response
{
    public class NewInvoiceResponse
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmout { get; set; }
        public decimal TotalCredits { get; set; }
    }
}