using System.Collections.Generic;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Domain.Entities;
namespace TheatricalPlayersRefactoringKata;

[XmlRoot(ElementName = "Statement", IsNullable = false)]
 public class StatementToXml
{ 
    [XmlElement(Order = 1)]
    public string Customer { get; set; }

    [XmlArray(ElementName ="Items",Order = 2)]
    [XmlArrayItem(ElementName ="Item")]
    public List<ItemToXMl> Items { get; set; } 

    [XmlElement(Order = 3)]
    public decimal AmountOwed { get; set; }

    [XmlElement(Order = 4)]
    public int EarnedCredits { get; set; }
}

public class ItemToXMl
{
    [XmlIgnore]
    public string Name { get; set; }

    [XmlElement(Order = 1)]
    public decimal AmountOwed { get; set; }

    [XmlElement(Order = 2)]
    public int EarnedCredits { get; set; }

    [XmlElement(Order = 3)]
    public int Seats { get; set; }
}