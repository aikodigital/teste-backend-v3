namespace TheatricalPlayersRefactoringKata.Domain.Entities

{
    public class Item
    {
        public int Id { get; set; }
        public decimal AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
        public int Seats { get; set; }
        public int? StatementId { get; set; }
        public Statement? Statement { get; set; }
    }
}
