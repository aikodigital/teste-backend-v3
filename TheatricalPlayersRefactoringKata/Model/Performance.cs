using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Model;

public class Performance
{
    private int _id;
    private string _playId;
    private decimal _amountOwed;
    private uint _earnedCredits;
    private uint _audience;
    
    [XmlIgnore]
    public int Id { get => _id; set => _id = value; }
    
    [XmlIgnore]
    public string PlayId { get => _playId; set => _playId = value; }
    
    [XmlElement("AmountOwed")]
    public decimal AmountOwed { get => _amountOwed; set => _amountOwed = value; }

    [XmlElement("EarnedCredits")]
    public uint EarnedCredits { get => _earnedCredits; set => _earnedCredits = value; }
    
    [XmlElement("Seats")]
    public uint Audience { get => _audience; set => _audience = value; }

    public Performance()
    {
        
    }

    public Performance(string playID, uint audience)
    {
        _playId = playID;
        _audience = audience;
        _amountOwed = 0;
        _earnedCredits = 0;
    }

}
