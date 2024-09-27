namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class InvoiceProcess : BaseEntity
{
    public int InvoiceId { get; set; }
    public bool Process { get; set; }
    public virtual Invoice Invoice { get; set; }
}
