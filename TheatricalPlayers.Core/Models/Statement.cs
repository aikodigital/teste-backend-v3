using System.Xml.Serialization;

namespace TheatricalPlayers.Core.Models;

[XmlRoot("Statement")]
public class Statement
{
  public Statement()
  {
    Plays = new List<StatementPlay>();
  }
  
  [XmlElement("Customer")]
  public string Customer { get; set; }
  [XmlArray("Items")]
  [XmlArrayItem("Item")]
  public List<StatementPlay> Plays { get; set; }
  [XmlElement("AmountOwed")]
  public decimal AmountOwed { get; set; }
  [XmlElement("EarnedCredits")]
  public decimal EarnedCredits { get; set; }
}

public class StatementPlay
{
  [XmlElement("Play")]
  public string PlayName { get; set; }
  
  [XmlElement("AmountOwed")]
  public decimal AmountOwed { get; set; }
  [XmlElement("EarnedCredits")]
  public decimal EarnedCredits { get; set; }
  [XmlElement("Seats")]
  public int Seats { get; set; }
}