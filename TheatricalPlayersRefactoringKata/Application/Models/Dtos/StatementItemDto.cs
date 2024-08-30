using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Application.Models.Dtos;

public class StatementItemDto
{
    [XmlElement("AmountOwed")]
    public decimal AmountOwed { get; set; }
    [XmlElement("EarnedCredits")]
    public decimal EarnedCredits { get; set; }
    [XmlElement("Seats")]
    public int Seats { get; set; }
}
