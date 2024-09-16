using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Entities;

public class StatementItemEntity
{
    [XmlIgnore]
    public string Name { get; set; } = null!;
    
    public decimal AmountOwed { get; set; }

    public int EarnedCredits { get; set; }

    public int Seats { get; set; }
}