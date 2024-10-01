namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Invoice : BaseEntity
{
    public string Customer { get; set; }

    public virtual ICollection<Performance> Performances { get; set; } = new List<Performance>();
}
  