namespace TheatricalPlayersRefactoringKata.Server.Database.DTOs.PerformanceHistory
{
    public class PerformanceHistoryDTO
    {
        public required int InvoiceHistoryId { get; set; }
        public required string PlayId { get; set; }
        public required int Audience { get; set; }
        public required decimal AmountOwed { get; set; }
        public required decimal EarnedCredits { get; set; }
    }
}