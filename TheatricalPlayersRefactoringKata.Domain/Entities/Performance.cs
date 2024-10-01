namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Performance : BaseEntity
{
    public int Audience { get; set; }
    public int PlayId { get; set; }

    public virtual Play Play { get; set; }
}
