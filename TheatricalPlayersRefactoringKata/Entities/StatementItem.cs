namespace TheatricalPlayersRefactoringKata.Entities;

public class StatementItem
{
    public int Id { get; set; }
    public int StatementId { get; set; }
    public decimal AmountOwed { get; set; }
    public int EarnedCredits { get; set; }
    public int Seats { get; set; } 
    public Statement Statement { get; set; }
}
