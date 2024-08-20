using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.InvoiceHistory;

namespace TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.PerformanceHistory
{
    public class PerformanceHistoryEntity
    {
        public int Id { get; set; }
        public int InvoiceHistoryId { get; set; }
        public string PlayId { get; set; }
        public int Audience { get; set; }
        public decimal AmountOwed { get; set; }
        public decimal EarnedCredits { get; set; }

        public InvoiceHistoryEntity InvoiceHistory { get; set; }

        public PerformanceHistoryEntity()
        { }
    }
}