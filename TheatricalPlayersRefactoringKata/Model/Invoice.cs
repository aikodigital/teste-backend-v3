using System.Collections.Generic;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Model;

[XmlRoot("Statement")]
public class Invoice
{
    private int _id;
    private decimal _amountOwed;
    private uint _earnedCredits;
    private string _customer;
    private List<Performance> _performances;

    [XmlIgnore]
    public int Id { get => _id; set => _id = value; }

    [XmlElement("Customer")]
    public string Customer { get => _customer; set => _customer = value; }
    
    [XmlArray("Items")]
    [XmlArrayItem("Item")]
    public List<Performance> Performances { get => _performances; set => _performances = value; }

    [XmlElement("AmountOwed")]
    public decimal AmountOwed { get => _amountOwed; set => _amountOwed = value; }

    [XmlElement("EarnedCredits")]
    public uint EarnedCredits { get => _earnedCredits; set => _earnedCredits = value; }


    public Invoice()
    {

    }
    
    public Invoice(string customer, List<Performance> performance)
    {
        _customer = customer;
        _performances = performance;
    }
}
