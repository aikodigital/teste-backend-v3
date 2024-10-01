namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Play : BaseEntity
{
    public string Name { get; set; }
    public int Lines { get; set; }
    public int TypeGenreId { get; set; }

    public virtual TypeGenre TypeGenre { get; set; }

    public virtual ICollection<CustomerPlaysStatement> CustomerPlaysStatement { get; set; }
    public virtual ICollection<Performance> Performances { get; set; }
}
