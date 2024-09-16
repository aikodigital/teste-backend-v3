using System.Collections.Generic;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Entities;

[XmlRoot("Statement")]
public class StatementEntity
{
    public string Customer { get; set; } = null!;

    [XmlArrayItem("Item")] 
    public List<StatementItemEntity> Items { get; set; } = new();

    public decimal AmountOwed { get; set; }

    public int EarnedCredits { get; set; }
}