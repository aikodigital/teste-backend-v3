namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class CustomerStatement : BaseEntity
{
    public string Customer { get; set; }
    public decimal TotalAmount { get; set; }
    public int VolumeCredits { get; set; }

    public virtual ICollection<CustomerPlaysStatement> CustomerPlaysStatement { get; set; } = new List<CustomerPlaysStatement>();
    public virtual ICollection<CustomerStatementProcess> CustomerStatementProcess { get; set; } = new List<CustomerStatementProcess>();
}