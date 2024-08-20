using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.PerformanceHistory;

namespace TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.InvoiceHistory
{
    public class InvoiceHistoryEntity
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public decimal TotalAmountOwed { get; set; }
        public decimal TotalEarnedCredits { get; set; }
        public string DateOfInvoice { get; set; }

        public ICollection<PerformanceHistoryEntity> PerformancesHistories { get; set; }

        public InvoiceHistoryEntity()
        { }
    }
}