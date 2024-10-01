namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class CustomerPlaysStatement : BaseEntity
{
    public int CustomerStatementId { get; set; }
    public int PlayId { get; set; }
    public decimal Amount { get; set; }
    public int TotalSeats { get; set; }

    public virtual CustomerStatement CustomerStatement { get; set; }
    public virtual Play Play { get; set; }
}