namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class CustomerStatementProcess : BaseEntity
{
    public int CustomerStatementId { get; set; }
    public bool Process { get; set; }
    public virtual CustomerStatement CustomerStatement { get; set; }
}
