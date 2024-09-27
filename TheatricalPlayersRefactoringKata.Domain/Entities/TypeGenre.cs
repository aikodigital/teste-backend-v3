namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class TypeGenre : BaseEntity
{
    public string Name { get; set; }
    public int BasePriceMultiplier { get; set; }
    public int? MaxAudience { get; set; }
    public int? ExtraFeePerAudience { get; set; }
    public int? BaseFeePerAudience { get; set; }
    public int? BonusFee { get; set; }

    public virtual ICollection<Play> Plays { get; set; }
}
