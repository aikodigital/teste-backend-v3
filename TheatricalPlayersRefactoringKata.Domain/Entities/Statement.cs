using System.Collections.Generic;
using System.Xml.Serialization;
namespace TheatricalPlayersRefactoringKata.Domain.Entities;

[XmlRoot(ElementName = "Statement", IsNullable = false)]
 public class Statement
{ 
    [XmlElement(Order = 1)]
    public string Customer { get; set; }

    [XmlArray(ElementName ="Items",Order = 2)]
    [XmlArrayItem(ElementName ="Item")]
    public List<Item> Items { get; set; } 

    [XmlElement(Order = 3)]
    public decimal AmountOwed { get; set; }

    [XmlElement(Order = 4)]
    public int EarnedCredits { get; set; }
}

public class Item
{
    [XmlElement(Order = 1)]
    public decimal AmountOwed { get; set; }

    [XmlElement(Order = 2)]
    public int EarnedCredits { get; set; }

    [XmlElement(Order = 3)]
    public int Seats { get; set; }
}