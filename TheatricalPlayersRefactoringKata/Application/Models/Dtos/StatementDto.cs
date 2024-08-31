using System.Collections.Generic;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Application.Models.Dtos;

[XmlRoot("Statement")]
public class StatementDto
{
    [XmlElement("Customer")]
    public string Customer { get; set; }
    [XmlArray("Items")]
    [XmlArrayItem("Item")]
    public List<StatementItemDto> Items { get; set; }
    [XmlElement("AmountOwed")]
    public decimal TotalAmountOwed { get; set; }
    [XmlElement("EarnedCredits")]
    public decimal TotalEarnedCredits { get; set; }
}
