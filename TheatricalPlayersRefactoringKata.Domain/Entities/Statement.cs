namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Statement
    {
        public int Id { get; set; }
        public required string Customer { get; set; }
        public required List<Item> Items { get; set; }
        public decimal TotalAmountOwed { get; set; }
        public int TotalEarnedCredits { get; set; }
    }
}
